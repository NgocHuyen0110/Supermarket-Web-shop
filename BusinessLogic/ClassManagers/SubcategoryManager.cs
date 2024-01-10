using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.ObjectClasses;

namespace BusinessLogic.ClassManagers
{
    public class SubcategoryManager
    {
        private readonly ISubcategoryDal _subcategoryDal;

        public SubcategoryManager(ISubcategoryDal subcategoryDal)
        {
            this._subcategoryDal = subcategoryDal;
        }
        public List<Subcategory> GetSubcategoriesByCategory(Category category)
        {
            return _subcategoryDal.GetSubcategoriesByCategory(category);
        }

    }
}
