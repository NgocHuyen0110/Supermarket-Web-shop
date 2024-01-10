using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ObjectClasses
{
    public class Subcategory
    {
        public string SubcategoryItem { get; set; }
        public int Id { get; set; }

        public Subcategory(string subcategoryItem)
        {
            SubcategoryItem = subcategoryItem;

        }

        public Subcategory(int id, string subcategoryItem)
        {
            Id = id;
            SubcategoryItem = subcategoryItem;
        }

        public Subcategory()
        {

        }

        public override string ToString()
        {
            return SubcategoryItem;
        }
    }
}
