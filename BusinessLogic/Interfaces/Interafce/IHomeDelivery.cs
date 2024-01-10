using BusinessLogic.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces.Interafce
{
    public interface IHomeDelivery
    {
        bool CreateHomeDeliveryAddress(DeliveryOption delivery);
        DeliveryAddress GetHomeDeliveryAddress(DeliveryAddress address);
        //Dictionary<TimeOnly, TimeSlot> GetTimeSlot(DeliveryOption delivery);
        Dictionary<TimeSpan, TimeSlot> GetTimeSlotsHomeDelivery();
        int CountTimeSlot(DeliveryOption delivery);
    }
}
