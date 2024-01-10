using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.ObjectClasses;

namespace BusinessLogic.DAL.InterfacesDAL
{
    public interface ICategoryDal
    {
        List<Category> GetCategories();

    }
}
