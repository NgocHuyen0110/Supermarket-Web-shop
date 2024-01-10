using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.ClassManagers;
using BusinessLogic.Enum;
using BusinessLogic.ObjectClasses;
using UnitTestRoberHejin.FakeData;

namespace UnitTestRoberHejin
{
    [TestClass]
    public class OrderTest
    {
        private OrderManager _orderManager = new OrderManager(new OrderDALFaker());

        [TestMethod]
        public void CreateOrder()
        {
            DateTime date = DateTime.Now;
            DeliveryOption deliveryOption = new HomeDelivery(new TimeSlot(new TimeSpan(9, 00, 00)), DateTime.Now.AddDays(1), new DeliveryAddress("Street"));
            User user = new Customer("Roxana", "Hejin", "Hovedgaden 1", 12345678,
                new Account("Roxana@gmail.com", "1234"));
            List<CartItem> cartItems = new List<CartItem>();
            cartItems.Add(new CartItem(new Item("Apple", 4, "500g", 100, new Subcategory("Fruit")), 2));
            cartItems.Add(new CartItem(new Item("Banana", 4, "500g", 100, new Subcategory("Fruit")), 2));
            Order order = new Order(date, (OrderStatus)0, user, deliveryOption, cartItems);
            _orderManager.CreateOrder(order);
            Assert.AreEqual(order, order);
        }

        [TestMethod]
        public void CreateOrderItemWithOutItem()
        {
            DateTime date = DateTime.Now;
            DeliveryOption deliveryOption = new HomeDelivery(new TimeSlot(new TimeSpan(12, 00, 00)), DateTime.Now.AddDays(1), new DeliveryAddress("Street"));
            User user = new Customer("Roxana", "Hejin", "Hovedgaden 1", 12345678,
                new Account("Roxana@gmail.com", "1234"));
            List<CartItem> cartItems = new List<CartItem>();
            Order order = new Order(date, (OrderStatus)0, user, deliveryOption, cartItems);
            bool result = _orderManager.CreateOrderItems(order);
            Assert.IsFalse(result);

        }
        [TestMethod]
        public void CreateOrderItem()
        {
            DateTime date = DateTime.Now;
            DeliveryOption deliveryOption = new HomeDelivery(new TimeSlot(new TimeSpan(3, 00, 00)), DateTime.Now.AddDays(1), new DeliveryAddress("Street"));
            User user = new Customer("Roxana", "Hejin", "Hovedgaden 1", 12345678,
                new Account("Roxana@gmail.com", "1234"));
            List<CartItem> cartItems = new List<CartItem>();
            cartItems.Add(new CartItem(new Item("Apple", 4, "500g", 100, new Subcategory("Fruit")), 2));
            cartItems.Add(new CartItem(new Item("Banana", 4, "500g", 100, new Subcategory("Fruit")), 2));
            Order order = new Order(date, (OrderStatus)0, user, deliveryOption, cartItems);
            bool result = _orderManager.CreateOrderItems(order);
            Assert.IsTrue(result);

        }

        //[TestMethod]
        //public bool UpDateOrder()
        //{
        //    DateTime date = DateTime.Now;
        //    DeliveryOption deliveryOption = new HomeDelivery(new TimeSlot(), DateTime.Now.AddDays(1), new DeliveryAddress("Street"));
        //    User user = new Customer("Roxana", "Hejin", "Hovedgaden 1", 12345678,
        //        new Account("Roxana@gmail.com", "1234"));
        //    List<CartItem> cartItems = new List<CartItem>();
        //    cartItems.Add(new CartItem(new Item("Apple", 4, "500g", 100, new Subcategory("Fruit")), 2));
        //    cartItems.Add(new CartItem(new Item("Banana", 4, "500g", 100, new Subcategory("Fruit")), 2));
        //    Order order = new Order(date, (OrderStatus)0, user, deliveryOption, cartItems);
        //    _orderManager.CreateOrderItems(order);
        //    Assert.IsTrue(result);
        //}
        [TestMethod]
        public void GetLastHomeOrderByUser()
        {
            //create oder 1
            DateTime date = DateTime.Now;
            DeliveryOption deliveryOption = new HomeDelivery(new TimeSlot(new TimeSpan(12, 00, 00)), DateTime.Now.AddDays(1), new DeliveryAddress("Street"));
            User user = new Customer("Roxana", "Hejin", "Hovedgaden 1", 12345678,
                new Account("Roxana@gmail.com", "1234"));
            List<CartItem> cartItems = new List<CartItem>();
            cartItems.Add(new CartItem(new Item("Apple", 4, "500g", 100, new Subcategory("Fruit")), 2));
            cartItems.Add(new CartItem(new Item("Banana", 4, "500g", 100, new Subcategory("Fruit")), 2));
            Order order = new Order(date, (OrderStatus)0, user, deliveryOption, cartItems);
            _orderManager.CreateOrder(order);

            //create order 2
            DateTime date2 = DateTime.Now;
            DeliveryOption deliveryOption2 = new PickUp(new TimeSlot(new TimeSpan(8, 00, 00)), DateTime.Now.AddDays(1), new DeliveryAddress("Street"));

            List<CartItem> cartItems2 = new List<CartItem>();
            cartItems2.Add(new CartItem(new Item("Apple", 4, "500g", 100, new Subcategory("Fruit")), 2));
            cartItems2.Add(new CartItem(new Item("Banana", 4, "500g", 100, new Subcategory("Fruit")), 2));
            Order order2 = new Order(date2, (OrderStatus)0, user, deliveryOption2, cartItems2);
            _orderManager.CreateOrder(order2);
            //Order result = _orderManager.GetLastHomeOrderByUser(user);
            Assert.AreEqual(order2, _orderManager.GetLastHomeOrderByUser(user));

        }

        [TestMethod]
        public void GetOrderItems()
        {
            DateTime date = DateTime.Now;
            DeliveryOption deliveryOption = new HomeDelivery(new TimeSlot(new TimeSpan(20, 00, 00)), DateTime.Now.AddDays(1), new DeliveryAddress("Street"));
            User user = new Customer("Roxana", "Hejin", "Hovedgaden 1", 12345678,
                new Account("Roxana@gmail.com", "1234"));
            List<CartItem> cartItems = new List<CartItem>();
            cartItems.Add(new CartItem(new Item("Apple", 4, "500g", 100, new Subcategory("Fruit")), 2));
            cartItems.Add(new CartItem(new Item("Banana", 4, "500g", 100, new Subcategory("Fruit")), 2));
            Order order = new Order(date, (OrderStatus)0, user, deliveryOption, cartItems);
            _orderManager.CreateOrder(order);
            _orderManager.CreateOrderItems(order);
            List<CartItem> result = _orderManager.GetOrderItems(order);
            Assert.AreEqual(cartItems, result);
        }

        [TestMethod]
        public void GetLastPickOrderByUser()
        {
            DateTime date = DateTime.Now;
            DeliveryOption deliveryOption = new HomeDelivery(new TimeSlot(new TimeSpan(13, 00, 00)), DateTime.Now.AddDays(1), new DeliveryAddress("Street"));
            User user = new Customer("Roxana", "Hejin", "Hovedgaden 1", 12345678,
                new Account("Roxana@gmail.com", "1234"));
            List<CartItem> cartItems = new List<CartItem>();
            cartItems.Add(new CartItem(new Item("Apple", 4, "500g", 100, new Subcategory("Fruit")), 2));
            cartItems.Add(new CartItem(new Item("Banana", 4, "500g", 100, new Subcategory("Fruit")), 2));
            Order order = new Order(date, (OrderStatus)0, user, deliveryOption, cartItems);
            _orderManager.CreateOrder(order);

            //create order 2
            DateTime date2 = DateTime.Now;
            DeliveryOption deliveryOption2 = new PickUp(new TimeSlot(new TimeSpan(15, 00, 00)), DateTime.Now.AddDays(1), new DeliveryAddress("Street"));

            List<CartItem> cartItems2 = new List<CartItem>();
            cartItems2.Add(new CartItem(new Item("Apple", 4, "500g", 100, new Subcategory("Fruit")), 2));
            cartItems2.Add(new CartItem(new Item("Banana", 4, "500g", 100, new Subcategory("Fruit")), 2));
            Order order2 = new Order(date2, (OrderStatus)0, user, deliveryOption2, cartItems2);
            _orderManager.CreateOrder(order2);
            //Order result = _orderManager.GetLastHomeOrderByUser(user);
            Assert.AreEqual(order2, _orderManager.GetLastPickOrderByUser(user));

        }

        [TestMethod]
        public void HistoryHomeDeliveryOrders()
        {
            // create order 1
            DateTime date = DateTime.Now;
            DeliveryOption deliveryOption = new HomeDelivery(new TimeSlot(new TimeSpan(8, 00, 00)), DateTime.Now.AddDays(1), new DeliveryAddress("Street"));
            User user = new Customer("Roxana", "Hejin", "Hovedgaden 1", 12345678,
                new Account("Roxana@gmail.com", "1234"));
            List<CartItem> cartItems = new List<CartItem>();
            cartItems.Add(new CartItem(new Item("Apple", 4, "500g", 100, new Subcategory("Fruit")), 2));
            cartItems.Add(new CartItem(new Item("Banana", 4, "500g", 100, new Subcategory("Fruit")), 2));
            Order order = new Order(date, (OrderStatus)0, user, deliveryOption, cartItems);
            _orderManager.CreateOrder(order);

            //create order 2 with same home delivery but different user
            DateTime date2 = DateTime.Now;
            DeliveryOption deliveryOption2 = new HomeDelivery(new TimeSlot(new TimeSpan(19, 00, 00)), DateTime.Now.AddDays(1), new DeliveryAddress("Street"));

            List<CartItem> cartItems2 = new List<CartItem>();
            cartItems2.Add(new CartItem(new Item("Apple", 4, "500g", 100, new Subcategory("Fruit")), 2));
            cartItems2.Add(new CartItem(new Item("Banana", 4, "500g", 100, new Subcategory("Fruit")), 2));
            User user2 = new Customer("Roxana", "Hejin", "Hovedgaden 1", 12345678,
                new Account("Huyen@gmail.com", "1234"));
            Order order2 = new Order(date2, (OrderStatus)0, user2, deliveryOption2, cartItems2);
            _orderManager.CreateOrder(order2);
            //Order result = _orderManager.GetLastHomeOrderByUser(user);
            List<Order> result = _orderManager.HistoryHomeDeliveryOrders(user);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void HistoryPickDeliveryOrders()
        {
            //create order 1
            DateTime date = DateTime.Now;
            DeliveryOption deliveryOption = new PickUp(new TimeSlot(new TimeSpan(19, 00, 00)), DateTime.Now.AddDays(1), new DeliveryAddress("Street"));
            User user = new Customer("Roxana", "Hejin", "Hovedgaden 1", 12345678,
                new Account("Roxana@gmail.com", "1234"));
            List<CartItem> cartItems = new List<CartItem>();
            cartItems.Add(new CartItem(new Item("Apple", 4, "500g", 100, new Subcategory("Fruit")), 2));
            cartItems.Add(new CartItem(new Item("Banana", 4, "500g", 100, new Subcategory("Fruit")), 2));
            Order order = new Order(date, (OrderStatus)0, user, deliveryOption, cartItems);
            _orderManager.CreateOrder(order);

            //create order 2 with same user but different delivery option
            DateTime date2 = DateTime.Now;
            DeliveryOption deliveryOption2 = new HomeDelivery(new TimeSlot(new TimeSpan(20, 00, 00)), DateTime.Now.AddDays(1), new DeliveryAddress("Street"));

            List<CartItem> cartItems2 = new List<CartItem>();
            cartItems2.Add(new CartItem(new Item("Apple", 4, "500g", 100, new Subcategory("Fruit")), 2));
            cartItems2.Add(new CartItem(new Item("Banana", 4, "500g", 100, new Subcategory("Fruit")), 2));
            Order order2 = new Order(date2, (OrderStatus)0, user, deliveryOption2, cartItems2);
            _orderManager.CreateOrder(order2);
            //Order result = _orderManager.GetLastHomeOrderByUser(user);
            List<Order> result = _orderManager.HistoryHomeDeliveryOrders(user);
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void HomeDeliveryOrders()
        {
            //create order 1
            DateTime date = DateTime.Now;
            DeliveryOption deliveryOption = new HomeDelivery(new TimeSlot(new TimeSpan(9, 00, 00)), DateTime.Now.AddDays(1), new DeliveryAddress("Street"));
            User user = new Customer("Roxana", "Hejin", "Hovedgaden 1", 12345678,
                new Account("Roxana@gmail.com", "1234"));
            List<CartItem> cartItems = new List<CartItem>();
            cartItems.Add(new CartItem(new Item("Apple", 4, "500g", 100, new Subcategory("Fruit")), 2));
            cartItems.Add(new CartItem(new Item("Banana", 4, "500g", 100, new Subcategory("Fruit")), 2));
            Order order = new Order(date, (OrderStatus)0, user, deliveryOption, cartItems);
            _orderManager.CreateOrder(order);

            //create order 2 with same user, same delivery option but different date
            DateTime date2 = DateTime.Now;
            DeliveryOption deliveryOption2 = new HomeDelivery(new TimeSlot(new TimeSpan(9, 00, 00)), DateTime.Now.AddDays(2), new DeliveryAddress("Street"));

            List<CartItem> cartItems2 = new List<CartItem>();
            cartItems2.Add(new CartItem(new Item("Apple", 4, "500g", 100, new Subcategory("Fruit")), 2));
            cartItems2.Add(new CartItem(new Item("Banana", 4, "500g", 100, new Subcategory("Fruit")), 2));
            Order order2 = new Order(date2, (OrderStatus)0, user, deliveryOption2, cartItems2);
            _orderManager.CreateOrder(order2);
            //Order result = _orderManager.GetLastHomeOrderByUser(user);
            List<Order> result = _orderManager.HistoryHomeDeliveryOrders(user);
            Assert.AreEqual(2, result.Count);
        }
        [TestMethod]
        public void PickDeliveryOrders()
        {
            //create order 1
            DateTime date = DateTime.Now;
            DeliveryOption deliveryOption = new PickUp(new TimeSlot(new TimeSpan(9, 00, 00)), DateTime.Now.AddDays(1), new DeliveryAddress("Street"));
            User user = new Customer("Roxana", "Hejin", "Hovedgaden 1", 12345678,
                new Account("Roxana@gmail.com", "1234"));
            List<CartItem> cartItems = new List<CartItem>();
            cartItems.Add(new CartItem(new Item("Apple", 4, "500g", 100, new Subcategory("Fruit")), 2));
            cartItems.Add(new CartItem(new Item("Banana", 4, "500g", 100, new Subcategory("Fruit")), 2));
            Order order = new Order(date, (OrderStatus)0, user, deliveryOption, cartItems);
            _orderManager.CreateOrder(order);

            //create order 2  different delivery option and user
            DateTime date2 = DateTime.Now;
            DeliveryOption deliveryOption2 = new HomeDelivery(new TimeSlot(new TimeSpan(9, 00, 00)), DateTime.Now.AddDays(1), new DeliveryAddress("Street"));
            User user2 = new Customer("Roxana", "Hejin", "Hovedgaden 1", 12345678,
                new Account("Huyen@gmail.com", "1234"));
            List<CartItem> cartItems2 = new List<CartItem>();
            cartItems2.Add(new CartItem(new Item("Apple", 4, "500g", 100, new Subcategory("Fruit")), 2));
            cartItems2.Add(new CartItem(new Item("Banana", 4, "500g", 100, new Subcategory("Fruit")), 2));
            Order order2 = new Order(date2, (OrderStatus)0, user, deliveryOption2, cartItems2);
            _orderManager.CreateOrder(order2);
            //Order result = _orderManager.GetLastHomeOrderByUser(user);
            List<Order> result = _orderManager.HistoryHomeDeliveryOrders(user);
            Assert.AreEqual(1, result.Count);
        }
    }
}
