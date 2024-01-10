using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ObjectClasses
{
    public class PickUp : DeliveryOption
    {
        public PickUp(TimeSlot timeSlot, DateTime deliveryDate, DeliveryAddress address) : base(timeSlot, deliveryDate, address)
        {
        }
        public PickUp()
        {

        }
        public override string ToString()
        {
            return TimeSlot.ToString() + " " + DeliveryDate.ToString();
        }
    }

}
