using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.ObjectClasses;

namespace BusinessLogic.ClassManagers
{
    public class OrderManager
    {
        private readonly IOrderDAL _orderDal;

        public OrderManager(IOrderDAL orderDal)
        {
            this._orderDal = orderDal;
        }
        public bool CreateOrder(Order order)
        {
            return _orderDal.CreateOrder(order);
        }
        public bool CreateOrderItems(Order order)
        {
            return _orderDal.CreateOrderItems(order);
        }

        public Order GetLastHomeOrderByUser(User user)
        {
            return _orderDal.GetLastHomeOrderByUser(user);
        }

        public List<CartItem> GetOrderItems(Order oder)
        {
            return _orderDal.GetOrderItems(oder);
        }

        public Order GetLastPickOrderByUser(User user)
        {
            return _orderDal.GetLastPickOrderByUser(user);
        }

        public List<Order> HistoryHomeDeliveryOrders(User user)
        {
            return _orderDal.HistoryHomeDeliveryOrders(user);
        }

        public List<Order> HistoryPickDeliveryOrders(User user)
        {
            return _orderDal.HistoryPickDeliveryOrders(user);
        }
        public List<Order> HomeDeliveryOrders()
        {
            return _orderDal.HomeDeliveryOrders();
        }
        public List<Order> PickDeliveryOrders()
        {
            return _orderDal.PickDeliveryOrders();
        }
        public bool UpdateOrder(Order order)
        {
            return _orderDal.UpdateOrder(order);
        }
    }
}
