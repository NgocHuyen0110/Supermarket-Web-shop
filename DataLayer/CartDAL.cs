using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.ObjectClasses;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using User = BusinessLogic.ObjectClasses.User;

namespace DataLayer
{
    public class CartDal : DatabaseHandler, ICartDal
    {
        public bool CreateCart(User user)
        {
            bool result;
            try
            {
                var sql = "INSERT INTO [dbo].[Cart] (UserID) VALUES ((Select c.UserID from [dbo].[Customer] as c inner join [dbo].[User] as u on c.UserID = u.ID  inner join [dbo].[Account] as a On u.AccountID = a.ID where Email = @Email))";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@Email", user.Account.Email);
                ExecuteNonQuery(cmd);
                result = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return result;
        }

        public List<CartItem> GetCartItems(User user, Cart cart)
        {
            SqlDataReader? reader = null;
            cart = new Cart();
            cart.CartItems = new List<CartItem>();
            try
            {
                var sql = "Select i.ID,i.ItemName, i.Unit, i.Price, i.AmountInStock, c.Quantity,c.ID as CartItemID, s.Subcategory from [dbo].[Item] as i inner join [dbo].[CartItem] as c on  i.ID = c.ItemID  inner join [dbo].[Cart] as c1 On c1.ID = c.CartID inner join [dbo].[Subcategory] as s on s.ID = i.SubcategoryID  where c1.UserID = @UserID";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@UserID", user.Id);
                reader = OpenExecuteReader(cmd);

                while (reader.Read())
                {

                    cart.CartItems.Add(new CartItem((new Item((int)reader["ID"], (string)reader["ItemName"], (decimal)reader["Price"], (string)reader["Unit"], (int)reader["AmountInStock"], new Subcategory((string)reader["Subcategory"]))), (int)reader["Quantity"], (int)reader["CartItemID"]));

                }
                return cart.CartItems;
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
        //public List<CartItem> GetCartItems(User user, Cart cart)
        //{
        //    SqlDataReader? reader = null;
        //    List<CartItem> cartItems = new List<CartItem>();
        //    try
        //    {
        //        var sql = "Select i.ID, i.ItemName, i.Unit, i.Price, c.Quantity, s.Subcategory from [dbo].[Item] as i inner join [dbo].[CartItem] as c on  i.ID = c.ItemID  inner join [dbo].[Cart] as c1 On c1.ID = c.CartID inner join [dbo].[Subcategory] as s on s.ID = i.SubcategoryID  where c1.UserID = @UserID";
        //        var cmd = new SqlCommand(sql, GetDbConnection());
        //        cmd.Parameters.AddWithValue("@UserID", user.Id);
        //        reader = OpenExecuteReader(cmd);
        //        while (reader.Read())
        //        {
        //            //if (reader == null)
        //            //{
        //            //    return ConvertFromD<List<CartItem>>(cart.CartItems);
        //            //}
        //            //cartItems.Add(new CartItem
        //            //{
        //            //    Item = new Item
        //            //    {
        //            //        ItemName = (string)reader["ItemName"],
        //            //        Unit = (string)reader["Unit"],
        //            //        Price = (decimal)reader["Price"]
        //            //    },
        //            //    Quantity = (int)reader["Quantity"]
        //            //});
        //            CartItem cartItem = new CartItem((new Item((int)reader["ID"], (string)reader["ItemName"], (string)reader["Unit"], (decimal)reader["Price"], new Subcategory((string)reader["Subcategory"]))), (int)reader["Quantity"]);
        //            cartItems.Add(cartItem);

        //        }
        //        return cartItems;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //    finally
        //    {
        //        CloseExecuteReader(reader);
        //    }
        //}

        public CartItem CheckCartItemExist(User user, Item item)
        {
            SqlDataReader? reader = null;
            CartItem? cartItem = null;
            try
            {
                var sql = "Select i.ID ,i.ItemName, i.Unit, i.Price, c.Quantity, s.Subcategory from [dbo].[Item] as i inner join [dbo].[CartItem] as c on  i.ID = c.ItemID  inner join [dbo].[Cart] as c1 On c1.ID = c.CartID inner join [dbo].[Subcategory] as s on s.ID = i.SubcategoryID  where c1.UserID = @UserID and i.ItemName = @ItemName";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@UserID", user.Id);
                cmd.Parameters.AddWithValue("@ItemName", item.ItemName);
                reader = OpenExecuteReader(cmd);
                while (reader.Read())
                {

                    cartItem = new CartItem((new Item((int)reader["ID"], (string)reader["ItemName"], (string)reader["Unit"], (decimal)reader["Price"], new Subcategory((string)reader["Subcategory"]))), (int)reader["Quantity"]);

                }
                return cartItem;
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
        //public bool AddItem(CartItem cartItem, User user)
        //{
        //    bool result;
        //    try
        //    {
        //        var sql = "Insert into [dbo].[CartItem] (ItemID, Quantity , CartID) values (@ItemID, @Quantity, (Select ID from [dbo].[Cart] where UserID = @UserID))";
        //        var cmd = new SqlCommand(sql, GetDbConnection());
        //        cmd.Parameters.AddWithValue("@ItemID", cartItem.Item.Id);
        //        cmd.Parameters.AddWithValue("@Quantity", cartItem.Quantity);
        //        cmd.Parameters.AddWithValue("@UserID", user.Id);
        //        ExecuteNonQuery(cmd);
        //        result = true;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }

        //    return result;
        //}
        public bool AddItem(Item item, User user, CartItem cartItem)
        {
            bool result;
            try
            {
                var sql = "Insert into [dbo].[CartItem] (ItemID, Quantity , CartID) values ((Select ID from [dbo].[Item] where ID =@ItemID), @Quantity, (Select ID from [dbo].[Cart] where UserID = @UserID))";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@ItemID", item.Id);
                cmd.Parameters.AddWithValue("@Quantity", cartItem.Quantity);
                cmd.Parameters.AddWithValue("@UserID", user.Id);
                ExecuteNonQuery(cmd);
                result = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return result;
        }

        public bool UpdateCart(User user, Item item, CartItem cartItem)
        {
            bool result;
            try
            {
                var sql = "Update [dbo].[CartItem] Set Quantity = @Quantity where ItemID = @ItemID and CartID = ((Select ID from [dbo].[Cart] where UserID = @UserID))";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@ItemID", item.Id);
                cmd.Parameters.AddWithValue("@Quantity", cartItem.Quantity);
                cmd.Parameters.AddWithValue("@UserID", user.Id);
                ExecuteNonQuery(cmd);
                result = true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return result;
        }
        public bool DeleteCartItem(User user, CartItem cartItem)
        {
            bool result;
            try
            {
                var sql = "Delete from [dbo].[CartItem] where ItemID = @ItemID and CartID = ((Select ID from [dbo].[Cart] where UserID = @UserID))";
                var cmd = new SqlCommand(sql, GetDbConnection());
                cmd.Parameters.AddWithValue("@ItemID", cartItem.Item.Id);
                cmd.Parameters.AddWithValue("@UserID", user.Id);
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
