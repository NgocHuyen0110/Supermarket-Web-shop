using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ObjectClasses
{
    public class Category
    {
        [Required]
        public string CatergoryItem { get; set; }
        public List<Subcategory> Subcatergories { get; set; }

        public Category(string categoryItem)
        {
            CatergoryItem = categoryItem;

        }

        public Category()
        {
            Subcatergories = new List<Subcategory>();
        }
        public override string ToString()
        {
            return CatergoryItem;
        }

    }
}
