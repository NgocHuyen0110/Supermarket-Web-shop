using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.ObjectClasses;
using Microsoft.Data.SqlClient;
using System;
using BusinessLogic.Enum;
using Microsoft.SqlServer.Management.Smo;
using User = BusinessLogic.ObjectClasses.User;
using Microsoft.SqlServer.Management.Sdk.Sfc;

namespace DataLayer
{
    public class OrderDAL : DatabaseHandler, IOrderDAL
    {

        public bool CreateOrder(Order order)
        {
            bool result;
            try
            {
                if (order.DeliverOption is HomeDelivery)
                {
                    var sql =
                        "Insert into [dbo].[Order] (OrderDate, OrderStatusID, UserID, TotalPrice, DeliveryOptionID, DeliveryDate, DeliveryTime, HomeDeliveryAddressID) VALUES (@OrderDate, (Select ID from [dbo].[OrderStatus] where Status =@Status), @UserID, @TotalPrice, @DeliveryOptionID, @DeliveryDate, @DeliveryTime, (Select ID from [dbo].[HomeDeliveryAddress] where Address = @Address))";
                    var cmd = new SqlCommand(sql, GetDbConnection());
                    cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Status", "PlacedOrder");
                    cmd.Parameters.AddWithValue("@UserID", order.User.Id);
                    cmd.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
                    cmd.Parameters.AddWithValue("@DeliveryOptionID", 1);
                    cmd.Parameters.AddWithValue("@DeliveryDate",
                        order.DeliverOption.DeliveryDate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@DeliveryTime", order.DeliverOption.TimeSlot.Time.ToString(@"hh\:mm"));
                    cmd.Parameters.AddWithValue("@Address", order.DeliverOption.Address.Address);
                    ExecuteNonQuery(cmd);
                    result = true;

                }
                else
                {
                    var sql =
                        "Insert into [dbo].[Order] (OrderDate, OrderStatusID, UserID, TotalPrice, DeliveryOptionID, DeliveryDate, DeliveryTime, PickUpAddressID) VALUES (@OrderDate, (Select ID from [dbo].[OrderStatus] where Status =@Status), @UserID, @TotalPrice, @DeliveryOptionID, @DeliveryDate, @DeliveryTime, (Select ID from [dbo].[PickUpAddress] where Address = @Address))";
                    var cmd = new SqlCommand(sql, GetDbConnection());
                    cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Status", "PlacedOrder");
                    cmd.Parameters.AddWithValue("@UserID", order.User.Id);
                    cmd.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);
                    cmd.Parameters.AddWithValue("@DeliveryOptionID", 2);
                    cmd.Parameters.AddWithValue("@DeliveryDate",
                        order.DeliverOption.DeliveryDate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@DeliveryTime", order.DeliverOption.TimeSlot.Time.ToString(@"hh\:mm"));
                    cmd.Parameters.AddWithValue("@Address", order.DeliverOption.Address.Address);
                    ExecuteNonQuery(cmd);
                    result = true;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return result;
        }


        public bool CreateOrderItems(Order order)
        {
            bool result;
            try
            {
                foreach (var item in order.OrderItems)
                {

                    var sql =
                        "Insert into [dbo].[OrderItem] (ItemID, Price, Amount, OrderID) VALUES (@ItemID, @Price, @Amount, (select TOP 1 ID  from [dbo].[Order] where UserID = @UserID order by ID desc ))";
                    var cmd = new SqlCommand(sql, GetDbConnection());
                    cmd.Parameters.AddWithValue("@ItemID", item.Item.Id);
                    cmd.Parameters.AddWithValue("@Price", item.Item.Price);
                    cmd.Parameters.AddWithValue("@Amount", item.Quantity);
                    cmd.Parameters.AddWithValue("@UserID", order.User.Id);
                    ExecuteNonQuery(cmd);
                    result = true;

                }

                result = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return result;
        }

        public Order GetLastHomeOrderByUser(User user)
        {
            SqlDataReader? reader = null;
            Order order = new Order();

            try
            {

                var sql =
                    " select  TOP 1  o.ID, o.OrderDate, o.TotalPrice, o.DeliveryDate, o.DeliveryTime,\r\n os.ID as osID , u.FirstName, u.LastName, u.PhoneNr, u.Address, a.Email,\r\n d.DeliveryOption, h.Address as DeliveryAddress\r\n from [dbo].[Order] as o \r\n inner join [dbo].[OrderStatus] as os on o.OrderStatusID = os.ID\r\n inner join [dbo].[Customer] as c on o.UserID = c.UserID \r\n inner join [dbo].[User] as u on c.UserID = u.ID \r\n inner join [dbo].[Account] as a on  u.AccountID = a.ID\r\n inner join [dbo].[DeliveryOption] as d on o.DeliveryOptionID = d.ID\r\n inner join [dbo].[HomeDeliveryAddress] as h on o.HomeDeliveryAddressID =h.ID \r\n where c.UserID = @UserID\r\n order by ID desc ";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@UserID", user.Id);
                reader = OpenExecuteReader(cmd);
                while (reader.Read())
                {

                    User user1 = new Customer((string)reader["FirstName"], (string)reader["LastName"],
                        (string)reader["Address"], (int)reader["PhoneNr"], new Account((string)reader["Email"]));
                    DeliveryOption deliveryOption = new HomeDelivery(new TimeSlot((TimeSpan)reader["DeliveryTime"]),
                        (DateTime)reader["DeliveryDate"], new DeliveryAddress((string)reader["DeliveryAddress"]));
                    order = new Order((int)reader["ID"], (DateTime)reader["OrderDate"], user,
                        (decimal)reader["TotalPrice"], deliveryOption, (OrderStatus)(int)reader["osID"]);
                }

                return order;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseExecuteReader(reader);
            }

        }

        public Order GetLastPickOrderByUser(User user)
        {
            SqlDataReader? reader = null;
            Order order = new Order();

            try
            {
                var sql =
                    "select  TOP 1  o.ID, o.OrderDate, o.TotalPrice, o.DeliveryDate, o.DeliveryTime,\r\n os.ID as osID, u.FirstName, u.LastName, u.PhoneNr, u.Address, a.Email,\r\n d.DeliveryOption, p.Address as DeliveryAddress\r\n from [dbo].[Order] as o \r\n inner join [dbo].[OrderStatus] as os on o.OrderStatusID = os.ID\r\n inner join [dbo].[PickUpAddress] as p on o.PickUpAddressID = p.ID\r\n inner join [dbo].[Customer] as c on o.UserID = c.UserID \r\n inner join [dbo].[User] as u on c.UserID = u.ID \r\n inner join [dbo].[Account] as a on  u.AccountID = a.ID\r\n inner join [dbo].[DeliveryOption] as d on o.DeliveryOptionID = d.ID\r\n where c.UserID = @UserID\r\n order by ID desc";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@UserID", user.Id);
                reader = OpenExecuteReader(cmd);
                while (reader.Read())
                {

                    User user1 = new Customer((string)reader["FirstName"], (string)reader["LastName"],
                        (string)reader["Address"], (int)reader["PhoneNr"], new Account((string)reader["Email"]));
                    DeliveryOption deliveryOption = new PickUp(new TimeSlot((TimeSpan)reader["DeliveryTime"]),
                        (DateTime)reader["DeliveryDate"], new DeliveryAddress((string)reader["DeliveryAddress"]));
                    order = new Order((int)reader["ID"], (DateTime)reader["OrderDate"], user,
                        (decimal)reader["TotalPrice"], deliveryOption, (OrderStatus)(int)reader["osID"]);
                }

                return order;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseExecuteReader(reader);
            }
        }

        public List<CartItem> GetOrderItems(Order oder)
        {
            SqlDataReader? reader = null;
            Order order = new Order();
            List<CartItem> orderItems = new List<CartItem>();
            try
            {

                var sql =
                    "  select oi.Amount, oi.Price, i.ItemName, i.Unit \r\n from [dbo].[OrderItem] as oi  \r\n inner join [dbo].[Item] as i on oi.ItemID = i.ID\r\n where OrderID = @OrderID";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@OrderID", oder.Id);
                reader = OpenExecuteReader(cmd);
                while (reader.Read())
                {
                    var c = new CartItem(
                        new((string)reader["ItemName"], (string)reader["Unit"], (decimal)reader["Price"]),
                        (int)reader["Amount"]);
                    orderItems.Add(c);

                }

                return orderItems;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseExecuteReader(reader);
            }
        }

        public List<Order> HistoryPickDeliveryOrders(User user)
        {
            SqlDataReader? reader = null;
            List<Order> orders = new List<Order>();
            Order order = new Order();
            try
            {
                var sql =
                    " select  o.ID, o.OrderDate, o.TotalPrice, o.DeliveryDate, o.DeliveryTime,\r\n os.ID as osID, u.FirstName, u.LastName, u.PhoneNr, u.Address, a.Email,\r\n d.DeliveryOption, p.Address as DeliveryAddress\r\n from [dbo].[Order] as o \r\n inner join [dbo].[OrderStatus] as os on o.OrderStatusID = os.ID\r\n inner join [dbo].[PickUpAddress] as p on o.PickUpAddressID = p.ID\r\n inner join [dbo].[Customer] as c on o.UserID = c.UserID \r\n inner join [dbo].[User] as u on c.UserID = u.ID \r\n inner join [dbo].[Account] as a on  u.AccountID = a.ID\r\n inner join [dbo].[DeliveryOption] as d on o.DeliveryOptionID = d.ID\r\n where c.UserID = 47\r\n order by ID desc ";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@UserID", user.Id);
                reader = OpenExecuteReader(cmd);
                while (reader.Read())
                {
                    User user1 = new Customer((string)reader["FirstName"], (string)reader["LastName"],
                        (string)reader["Address"], (int)reader["PhoneNr"], new Account((string)reader["Email"]));
                    DeliveryOption deliveryOption = new PickUp(new TimeSlot((TimeSpan)reader["DeliveryTime"]),
                        (DateTime)reader["DeliveryDate"], new DeliveryAddress((string)reader["DeliveryAddress"]));
                    order = new Order((int)reader["ID"], (DateTime)reader["OrderDate"], user1,
                        (decimal)reader["TotalPrice"], deliveryOption, (OrderStatus)(int)reader["osID"]);
                    orders.Add(order);

                }
                return orders;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseExecuteReader(reader);
            }
        }

        public List<Order> HistoryHomeDeliveryOrders(User user)
        {
            SqlDataReader? reader = null;
            List<Order> orders = new List<Order>();
            Order order = new Order();
            try
            {
                var sql =
                    " select  o.ID, o.OrderDate, o.TotalPrice, o.DeliveryDate, o.DeliveryTime,\r\n os.ID as osID, u.FirstName, u.LastName, u.PhoneNr, u.Address, a.Email,\r\n d.DeliveryOption, h.Address as DeliveryAddress\r\n from [dbo].[Order] as o \r\n inner join [dbo].[OrderStatus] as os on o.OrderStatusID = os.ID\r\n inner join [dbo].[Customer] as c on o.UserID = c.UserID \r\n inner join [dbo].[User] as u on c.UserID = u.ID \r\n inner join [dbo].[Account] as a on  u.AccountID = a.ID\r\n inner join [dbo].[DeliveryOption] as d on o.DeliveryOptionID = d.ID\r\n inner join [dbo].[HomeDeliveryAddress] as h on o.HomeDeliveryAddressID =h.ID \r\n where c.UserID = @UserID\r\n order by ID desc  ";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@UserID", user.Id);
                reader = OpenExecuteReader(cmd);
                while (reader.Read())
                {
                    User user1 = new Customer((string)reader["FirstName"], (string)reader["LastName"],
                        (string)reader["Address"], (int)reader["PhoneNr"], new Account((string)reader["Email"]));
                    DeliveryOption deliveryOption = new HomeDelivery(new TimeSlot((TimeSpan)reader["DeliveryTime"]),
                        (DateTime)reader["DeliveryDate"], new DeliveryAddress((string)reader["DeliveryAddress"]));
                    order = new Order((int)reader["ID"], (DateTime)reader["OrderDate"], user1,
                        (decimal)reader["TotalPrice"], deliveryOption, (OrderStatus)(int)reader["osID"]);
                    orders.Add(order);

                }
                return orders;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseExecuteReader(reader);
            }
        }
        public List<Order> HomeDeliveryOrders()
        {
            SqlDataReader? reader = null;
            List<Order> orders = new List<Order>();
            Order order = new Order();
            try
            {
                var sql =
                    " select  o.ID, o.OrderDate, o.TotalPrice, o.DeliveryDate, o.DeliveryTime,\r\n os.ID as osID, u.FirstName, u.LastName, u.PhoneNr, u.Address, a.Email,\r\n d.DeliveryOption, h.Address as DeliveryAddress\r\n from [dbo].[Order] as o \r\n inner join [dbo].[OrderStatus] as os on o.OrderStatusID = os.ID\r\n inner join [dbo].[Customer] as c on o.UserID = c.UserID \r\n inner join [dbo].[User] as u on c.UserID = u.ID \r\n inner join [dbo].[Account] as a on  u.AccountID = a.ID\r\n inner join [dbo].[DeliveryOption] as d on o.DeliveryOptionID = d.ID\r\n inner join [dbo].[HomeDeliveryAddress] as h on o.HomeDeliveryAddressID =h.ID \r\n order by ID desc  ";
                var cmd = new SqlCommand(sql, GetDbConnection());
                reader = OpenExecuteReader(cmd);
                while (reader.Read())
                {
                    User user1 = new Customer((string)reader["FirstName"], (string)reader["LastName"],
                        (string)reader["Address"], (int)reader["PhoneNr"], new Account((string)reader["Email"]));
                    DeliveryOption deliveryOption = new HomeDelivery(new TimeSlot((TimeSpan)reader["DeliveryTime"]),
                        (DateTime)reader["DeliveryDate"], new DeliveryAddress((string)reader["DeliveryAddress"]));
                    order = new Order((int)reader["ID"], (DateTime)reader["OrderDate"], user1,
                        (decimal)reader["TotalPrice"], deliveryOption, (OrderStatus)(int)reader["osID"]);
                    orders.Add(order);

                }
                return orders;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseExecuteReader(reader);
            }
        }
        public List<Order> PickDeliveryOrders()
        {
            SqlDataReader? reader = null;
            List<Order> orders = new List<Order>();
            Order order = new Order();
            try
            {
                var sql =
                    " select  o.ID, o.OrderDate, o.TotalPrice, o.DeliveryDate, o.DeliveryTime,\r\n os.ID as osID, u.FirstName, u.LastName, u.PhoneNr, u.Address, a.Email,\r\n d.DeliveryOption, p.Address as DeliveryAddress\r\n from [dbo].[Order] as o \r\n inner join [dbo].[OrderStatus] as os on o.OrderStatusID = os.ID\r\n inner join [dbo].[PickUpAddress] as p on o.PickUpAddressID = p.ID\r\n inner join [dbo].[Customer] as c on o.UserID = c.UserID \r\n inner join [dbo].[User] as u on c.UserID = u.ID \r\n inner join [dbo].[Account] as a on  u.AccountID = a.ID\r\n inner join [dbo].[DeliveryOption] as d on o.DeliveryOptionID = d.ID\r\n order by ID desc ";
                var cmd = new SqlCommand(sql, GetDbConnection());
                reader = OpenExecuteReader(cmd);
                while (reader.Read())
                {
                    User user1 = new Customer((string)reader["FirstName"], (string)reader["LastName"],
                        (string)reader["Address"], (int)reader["PhoneNr"], new Account((string)reader["Email"]));
                    DeliveryOption deliveryOption = new PickUp(new TimeSlot((TimeSpan)reader["DeliveryTime"]),
                        (DateTime)reader["DeliveryDate"], new DeliveryAddress((string)reader["DeliveryAddress"]));
                    order = new Order((int)reader["ID"], (DateTime)reader["OrderDate"], user1,
                        (decimal)reader["TotalPrice"], deliveryOption, (OrderStatus)(int)reader["osID"]);
                    orders.Add(order);

                }
                return orders;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseExecuteReader(reader);
            }
        }

        public bool UpdateOrder(Order order)
        {
            bool result;
            try
            {
                var sql = "Update [dbo].[Order] set OrderStatusID = @Status where ID = @ID";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@ID", order.Id);
                cmd.Parameters.AddWithValue("@Status", order.OrderStatus);
                ExecuteNonQuery(cmd);
                result = true;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }


    }
}
