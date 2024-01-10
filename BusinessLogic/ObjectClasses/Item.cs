using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ObjectClasses
{
    public class Item
    {
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public int AmountInStock { get; set; }
        public Subcategory Subcategory { get; set; }
        public int Id { get; set; }
        public Item(int id, string itemName, decimal price, string unit, int amountInStock, Subcategory subcategory)
        {
            ItemName = itemName;
            Price = price;
            Unit = unit;
            AmountInStock = amountInStock;
            Subcategory = subcategory;
            Id = id;
        }
        public Item(string itemName, decimal price, string unit, int amountInStock, Subcategory subcategory)
        {
            ItemName = itemName;
            Price = price;
            Unit = unit;
            AmountInStock = amountInStock;
            Subcategory = subcategory;
        }

        public Item(string itemName, string unit, decimal price)
        {
            ItemName = itemName;
            Unit = unit;
            Price = price;

        }
        public Item()
        {

        }

        public Item(int id, string itemName, string unit, decimal price, Subcategory subcategory)
        {
            ItemName = itemName;
            Unit = unit;
            Price = price;
            Subcategory = subcategory;
            Id = id;

        }



    }
}
