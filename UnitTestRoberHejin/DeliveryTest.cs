using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.ClassManagers;
using BusinessLogic.ObjectClasses;
using UnitTestRoberHejin.FakeData;

namespace UnitTestRoberHejin
{
    [TestClass]
    public class DeliveryTest
    {
        private DeliveryManager _deliveryManager = new DeliveryManager(new DeliveryFakerDAL());

        [TestMethod]
        public void CreateHomeDeliveryAddressWithExistAddress()
        {
            DeliveryOption delivery = new HomeDelivery(new TimeSlot(new TimeSpan(9, 00, 00)), (DateTime.Today).AddDays(1), new DeliveryAddress("Wattstraat 23"));
            bool result = _deliveryManager.CreateHomeDeliveryAddress(delivery);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void CreateHomeDeliveryAddressWithNewAddress()
        {
            DeliveryOption delivery = new HomeDelivery(new TimeSlot(new TimeSpan(11, 00, 00)), DateTime.Now.AddDays(1), new DeliveryAddress("Eliasstraat 19"));
            bool result = _deliveryManager.CreateHomeDeliveryAddress(delivery);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void GetHomeDeliveryAddressWithExistAddress()
        {
            DeliveryAddress address = new DeliveryAddress("Wattstraat 23");
            DeliveryAddress result = _deliveryManager.GetHomeDeliveryAddress(address);
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetHomeDeliveryAddressWithNewAddress()
        {
            DeliveryAddress address = new DeliveryAddress("Eliasstraat 19");
            DeliveryAddress result = _deliveryManager.GetHomeDeliveryAddress(address);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetAllAddress()
        {
            DeliveryOption delivery = new PickUp(new TimeSlot(), DateTime.Now.AddDays(1), new DeliveryAddress("John F. Kennedylaan 5"));
            _deliveryManager.CreateHomeDeliveryAddress(delivery);
            List<DeliveryAddress> result = _deliveryManager.GetAllAddress();
            Assert.AreEqual(1, result.Count);
        }
        [TestMethod]
        public void CountTimeSlotWithExistTimeSlot()
        {
            DeliveryOption delivery = new HomeDelivery(new TimeSlot(new TimeSpan(9, 00, 00)), (DateTime.Today).AddDays(1), new DeliveryAddress("Eliasstraat 19"));
            int result = _deliveryManager.CountTimeSlot(delivery);
            Assert.AreEqual(3, result);
        }
    }
}
