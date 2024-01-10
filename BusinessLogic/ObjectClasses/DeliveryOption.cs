using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ObjectClasses
{
    public class DeliveryOption
    {
        [Required]
        public DeliveryAddress Address { get; set; }
        [Required, DataType(DataType.Time)]
        public TimeSlot TimeSlot { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set; }
        public DeliveryOption(TimeSlot timeSlot, DateTime deliveryDate)
        {

            TimeSlot = timeSlot;
            DeliveryDate = deliveryDate;
        }
        public DeliveryOption(TimeSlot timeSlot, DateTime deliveryDate, DeliveryAddress address)
        {
            Address = address;
            TimeSlot = timeSlot;
            DeliveryDate = deliveryDate;
        }

        public DeliveryOption()
        {

        }


    }
}
