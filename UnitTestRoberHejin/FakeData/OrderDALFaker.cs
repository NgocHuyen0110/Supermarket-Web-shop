using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.ObjectClasses;

namespace UnitTestRoberHejin.FakeData
{
    public class OrderDALFaker : IOrderDAL
    {
        private Dictionary<int, Order> orders;
        private Dictionary<int, CartItem> orderItems;

        public OrderDALFaker()
        {

            orders = new Dictionary<int, Order>();


        }
        public bool CreateOrder(Order order)
        {
            order.Id = orders.Count + 1;
            orders.Add(order.Id, order);
            GetTotalPrice(order);
            return orders.ContainsKey(order.Id);

        }

        public bool CreateOrderItems(Order order)
        {
            if (order.OrderItems.Count != null)
            {
                foreach (var item in order.OrderItems)
                {
                    item.ID = orderItems.Count + 1;
                    orderItems.Add(item.ID, item);
                }
                order.OrderItems = orderItems.Values.ToList();
                return true;
            }

            return false;
        }
        public decimal GetTotalPrice(Order order)
        {
            decimal total = 0;
            foreach (var c in order.OrderItems)
            {
                total += c.Item.Price * c.Quantity;
            }
            return total;
        }

        public bool UpdateOrder(Order order)
        {
            if (orders.ContainsKey(order.Id))
            {
                orders[order.Id] = order;
                return true;
            }
            return false;
        }

        public Order GetLastHomeOrderByUser(User user)
        {
            Dictionary<int, Order> orders1 = new Dictionary<int, Order>();
            foreach (var o in orders)
            {
                if (o.Value.User.Account.Email == user.Account.Email)
                {
                    orders1.Add(o.Key, o.Value);
                }
            }
            return orders1.Last().Value;

        }

        public List<CartItem> GetOrderItems(Order order)
        {
            foreach (var o in orders)
            {
                if (o.Value.Id == order.Id)
                {
                    return o.Value.OrderItems;
                }
            }
            return null;
        }

        public Order GetLastPickOrderByUser(User user)
        {
            Dictionary<int, Order> orders1 = new Dictionary<int, Order>();
            foreach (var o in orders)
            {
                if (o.Value.User.Account.Email == user.Account.Email && o.Value.DeliverOption is PickUp)
                {
                    orders1.Add(o.Key, o.Value);
                }
            }
            return orders1.Last().Value;
        }

        public List<Order> HistoryHomeDeliveryOrders(User user)
        {
            Dictionary<int, Order> orders1 = new Dictionary<int, Order>();
            foreach (var o in orders)
            {
                if (o.Value.DeliverOption is HomeDelivery && o.Value.User.Account.Email == user.Account.Email)
                {
                    orders1.Add(o.Key, o.Value);
                }

            }
            return orders1.Values.ToList();
        }

        public List<Order> HistoryPickDeliveryOrders(User user)
        {
            Dictionary<int, Order> orders1 = new Dictionary<int, Order>();
            foreach (var o in orders)
            {
                if (o.Value.DeliverOption is PickUp && o.Value.User.Account.Email == user.Account.Email)
                {
                    orders1.Add(o.Key, o.Value);
                }

            }
            return orders1.Values.ToList();
        }

        public List<Order> HomeDeliveryOrders()
        {
            Dictionary<int, Order> orders1 = new Dictionary<int, Order>();
            foreach (var o in orders)
            {
                if (o.Value.DeliverOption is HomeDelivery)
                {
                    orders1.Add(o.Key, o.Value);
                }
            }
            return orders1.Values.ToList();
        }

        public List<Order> PickDeliveryOrders()
        {
            Dictionary<int, Order> orders1 = new Dictionary<int, Order>();
            foreach (var o in orders)
            {
                if (o.Value.DeliverOption is PickUp)
                {
                    orders1.Add(o.Key, o.Value);
                }
            }
            return orders1.Values.ToList();
        }

    }
}
