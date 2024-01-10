using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.ObjectClasses;

namespace BusinessLogic.ClassManagers
{
    public class CategoryManager
    {
        private readonly ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            this._categoryDal = categoryDal;
        }

        public List<Category> GetCategories()
        {
            return _categoryDal.GetCategories();
        }
    }
}
