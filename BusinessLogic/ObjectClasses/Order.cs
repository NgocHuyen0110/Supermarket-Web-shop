using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Enum;

namespace BusinessLogic.ObjectClasses
{
    public class Order
    {
        public int Id { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        [Required]
        public OrderStatus OrderStatus { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        public DeliveryOption DeliverOption { get; set; }
        [Required]
        public List<CartItem> OrderItems { get; set; }

        public Order(DateTime orderDate, OrderStatus orderStatus, User user, decimal totalPrice, DeliveryOption deliverOption)
        {
            OrderDate = orderDate;
            OrderStatus = orderStatus;
            User = user;
            TotalPrice = totalPrice;
            DeliverOption = deliverOption;
            OrderItems = new List<CartItem>();
        }
        public Order(DateTime orderDate, User user, decimal totalPrice, DeliveryOption deliverOption, List<CartItem> ordItems)
        {
            OrderDate = orderDate;
            User = user;
            TotalPrice = totalPrice;
            DeliverOption = deliverOption;
            OrderItems = ordItems;
            OrderItems = new List<CartItem>();
        }
        public Order(DateTime orderDate, OrderStatus orderStatus, User user, DeliveryOption deliverOption, List<CartItem> ordItems)
        {
            OrderDate = orderDate;
            User = user;
            DeliverOption = deliverOption;
            OrderItems = ordItems;
            OrderItems = new List<CartItem>();
        }
        public Order(int id, DateTime orderDate, User user, decimal totalPrice, DeliveryOption deliverOption, OrderStatus orderStatus, List<CartItem> ordItems)
        {
            Id = id;
            OrderDate = orderDate;
            User = user;
            TotalPrice = totalPrice;
            DeliverOption = deliverOption;
            OrderItems = ordItems;
            OrderItems = new List<CartItem>();
        }
        public Order(int id, DateTime orderDate, User user, decimal totalPrice, DeliveryOption deliverOption, OrderStatus orderStatus)
        {
            Id = id;
            OrderDate = orderDate;
            User = user;
            TotalPrice = totalPrice;
            DeliverOption = deliverOption;
            OrderItems = new List<CartItem>();
        }
        public Order()
        {

        }
        public override string ToString()
        {
            return OrderStatus.ToString();
        }
    }
}
