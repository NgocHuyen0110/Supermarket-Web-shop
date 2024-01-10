using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.ObjectClasses;

namespace UnitTestRoberHejin.FakeData
{
    public class DeliveryFakerDAL : IDeliveryDAL
    {
        Dictionary<string, DeliveryAddress> _deliveries;
        public List<DeliveryOption> deliveries;
        public DeliveryFakerDAL()
        {
            _deliveries = new Dictionary<string, DeliveryAddress>();
            deliveries = new List<DeliveryOption>();
            DeliveryOption deliveryOption = new HomeDelivery(new TimeSlot(new TimeSpan(9, 00, 00)), DateTime.Now.AddDays(1), new DeliveryAddress("Wattstraat 23"));
            _deliveries.Add(deliveryOption.Address.Address, deliveryOption.Address);

        }
        public bool CreateHomeDeliveryAddress(DeliveryOption delivery)
        {
            if (GetHomeDeliveryAddress(delivery.Address) != null)
            {
                _deliveries.Add(delivery.Address.Address, delivery.Address);
                return true;
            }
            return false;
        }

        public DeliveryAddress GetHomeDeliveryAddress(DeliveryAddress address)
        {
            return _deliveries.ContainsKey(address.Address) ? _deliveries[address.Address] : null;
        }

        public List<DeliveryAddress> GetAllAddress()
        {
            return _deliveries.Values.ToList();
        }

        public int CountTimeSlot(DeliveryOption delivery)
        {
            int count = 0;
            DeliveryOption deliveryOption4 = new HomeDelivery(new TimeSlot(new TimeSpan(9, 00, 00)), (DateTime.Today).AddDays(1), new DeliveryAddress("Eliasstraat 19"));
            deliveries.Add(deliveryOption4);
            DeliveryOption deliveryOption1 = new HomeDelivery(new TimeSlot(new TimeSpan(9, 00, 00)), (DateTime.Today).AddDays(1), new DeliveryAddress("Achterstraat 19"));
            deliveries.Add(deliveryOption1);
            DeliveryOption deliveryOption3 = new HomeDelivery(new TimeSlot(new TimeSpan(9, 00, 00)), (DateTime.Today).AddDays(1), new DeliveryAddress("Quintenstraat 19"));
            deliveries.Add(deliveryOption3);
            foreach (var d in deliveries)
            {
                if (d.TimeSlot.Time == delivery.TimeSlot.Time && d.DeliveryDate == delivery.DeliveryDate)
                {
                    count += 1;
                }

            }
            return count;
        }
    }
}
