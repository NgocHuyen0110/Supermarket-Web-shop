using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.ObjectClasses;

namespace BusinessLogic.InterfacesDAL
{
    public interface IUserDal
    {
        bool CreateUser(User user);
        bool AssignEmployee(User user);
        bool AssignCustomer(User user);
        User GetEmployeeByEmail(string email);
        User GetCustomerByEmail(string email);
        bool UpdateUser(User user);
        User CheckUserEmailUnit(string email);

    }
}
