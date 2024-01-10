using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.ObjectClasses;

namespace BusinessLogic.DAL.InterfacesDAL
{
    public interface IDeliveryDAL
    {
        bool CreateHomeDeliveryAddress(DeliveryOption delivery);
        DeliveryAddress GetHomeDeliveryAddress(DeliveryAddress address);
        List<DeliveryAddress> GetAllAddress();
        int CountTimeSlot(DeliveryOption delivery);
    }
}
