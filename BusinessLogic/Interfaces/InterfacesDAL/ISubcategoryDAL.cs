using BusinessLogic.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DAL.InterfacesDAL
{
    public interface ISubcategoryDal
    {
        List<Subcategory> GetSubcategoriesByCategory(Category category);
    }
}
