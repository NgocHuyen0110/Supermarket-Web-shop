using BusinessLogic.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces.Interafce
{
    public interface IPickUp
    {
        List<DeliveryAddress> GetAllAddress();
        //Dictionary<TimeOnly, TimeSlot> GetTimeSlot(DeliveryOption delivery);
        int CountTimeSlot(DeliveryOption delivery);
        Dictionary<TimeSpan, TimeSlot> GetTimeSlotsPickUp();
    }
}
