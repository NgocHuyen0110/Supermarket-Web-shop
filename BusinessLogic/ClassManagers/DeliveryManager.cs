using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.Interfaces.Interafce;
using BusinessLogic.ObjectClasses;

namespace BusinessLogic.ClassManagers
{
    public class DeliveryManager : IHomeDelivery, IPickUp
    {
        private readonly IDeliveryDAL _deliveryDal;

        public DeliveryManager(IDeliveryDAL deliveryDal)
        {
            this._deliveryDal = deliveryDal;
        }

        public bool CreateHomeDeliveryAddress(DeliveryOption delivery)
        {

            return _deliveryDal.CreateHomeDeliveryAddress(delivery);

        }

        public DeliveryAddress GetHomeDeliveryAddress(DeliveryAddress address)
        {
            return _deliveryDal.GetHomeDeliveryAddress(address);
        }

        public List<DeliveryAddress> GetAllAddress()
        {
            return _deliveryDal.GetAllAddress();
        }

        public Dictionary<TimeSpan, TimeSlot> GetTimeSlotsHomeDelivery()
        {
            int gapBetweenTimeSlots = 15;
            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(21, 0, 0);
            Dictionary<TimeSpan, TimeSlot> timeSlots = new Dictionary<TimeSpan, TimeSlot>();
            for (TimeSpan time = startTime; time < endTime; time = time.Add(new TimeSpan(0, gapBetweenTimeSlots, 0)))
            {
                timeSlots.Add(time, new TimeSlot(time));
            }
            return timeSlots;
        }

        public Dictionary<TimeSpan, TimeSlot> GetTimeSlotsPickUp()
        {
            int gapBetweenTimeSlots = 30;
            TimeSpan startTime = new TimeSpan(8, 0, 0);
            TimeSpan endTime = new TimeSpan(22, 0, 0);
            Dictionary<TimeSpan, TimeSlot> timeSlots = new Dictionary<TimeSpan, TimeSlot>();
            for (TimeSpan time = startTime; time < endTime; time = time.Add(new TimeSpan(0, gapBetweenTimeSlots, 0)))
            {
                timeSlots.Add(time, new TimeSlot(time));
            }
            return timeSlots;
        }
        public int CountTimeSlot(DeliveryOption delivery)
        {
            return _deliveryDal.CountTimeSlot(delivery);
        }

        //public Dictionary<TimeOnly, TimeSlot> GetTimeSlot(DeliveryOption delivery)
        //{
        //    Dictionary<TimeOnly, TimeSlot> timeSlots = new Dictionary<TimeOnly, TimeSlot>();
        //    if (delivery is HomeDelivery)
        //    {
        //        foreach (var timeSlot in GetTimeSlotsHomeDelivery())
        //        {
        //            delivery.TimeSlot = timeSlot.Value;
        //            if (CountTimeSlot(delivery) < 5)
        //            {
        //                timeSlots.Add(timeSlot.Key, timeSlot.Value);
        //            }
        //            return timeSlots;
        //        }
        //    }
        //    else
        //    {
        //        foreach (var timeSlot in GetTimeSlotsPickUp())
        //        {
        //            delivery.TimeSlot = timeSlot.Value;
        //            if (CountTimeSlot(delivery) < 2)
        //            {
        //                timeSlots.Add(timeSlot.Key, timeSlot.Value);
        //            }
        //            return timeSlots;
        //        }
        //    }
        //    return timeSlots;
        //}

    }
}


