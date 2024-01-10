using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ObjectClasses
{
    public class DeliveryAddress
    {
        [Required]
        public string Address { get; set; }
        public DeliveryAddress(string address)
        {
            Address = address;

        }

        public DeliveryAddress()
        {

        }
        public override string ToString()
        {
            return Address;
        }
    }
}
