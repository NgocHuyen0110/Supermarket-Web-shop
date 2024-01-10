using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.ObjectClasses;

namespace BusinessLogic.DAL.InterfacesDAL
{
    public interface IOrderDAL
    {
        bool CreateOrder(Order order);
        bool CreateOrderItems(Order order);
        bool UpdateOrder(Order order);
        Order GetLastHomeOrderByUser(User user);
        List<CartItem> GetOrderItems(Order oder);
        Order GetLastPickOrderByUser(User user);
        List<Order> HistoryHomeDeliveryOrders(User user);
        List<Order> HistoryPickDeliveryOrders(User user);
        List<Order> HomeDeliveryOrders();
        List<Order> PickDeliveryOrders();
    }
}
