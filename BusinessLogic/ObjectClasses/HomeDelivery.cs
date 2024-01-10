using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ObjectClasses
{
    public class HomeDelivery : DeliveryOption
    {
        public HomeDelivery(TimeSlot timeSlot, DateTime deliveryDate, DeliveryAddress address) : base(timeSlot, deliveryDate, address)
        {
        }
        public HomeDelivery(TimeSlot timeSlot, DateTime deliveryDate) : base(timeSlot, deliveryDate)
        {
        }
        public HomeDelivery()
        {

        }
        public override string ToString()
        {
            return TimeSlot.ToString() + " " + DeliveryDate.ToString();
        }
    }

}
