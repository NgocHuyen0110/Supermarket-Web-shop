using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ObjectClasses
{
    public class Cart
    {
        public int Id { get; set; }
        public List<CartItem> CartItems { get; set; }
        public Decimal TotalPrice { get; set; }

        public Cart()
        {
            CartItems = new List<CartItem>();
        }
        public override string ToString()
        {
            return CartItems.ToString();
        }


    }
}
