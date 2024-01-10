using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ObjectClasses
{
    public class CartItem
    {
        public int ID { get; set; }
        public Item Item { get; set; }
        [Required]
        public int Quantity { get; set; }

        public CartItem(Item item, int quantity)
        {
            Item = item;
            Quantity = quantity;

        }

        public CartItem(int quanity)
        {
            Quantity = quanity;
        }

        public CartItem(Item item, int quantity, int id)
        {
            ID = id;
            Item = item;
            Quantity = quantity;
        }

        public CartItem()
        {

        }
        public override string ToString()
        {
            return Item.ToString() + " " + Quantity;
        }


    }
}
