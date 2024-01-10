using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Enum;
using BusinessLogic.InterfacesDAL;
using BusinessLogic.ObjectClasses;

namespace UnitTestRoberHejin.FakeData
{
    public class UserDalFaker : IUserDal
    {
        private Dictionary<string, User> _users;
        private Dictionary<string, User> employees;
        private Dictionary<string, User> customers;

        public UserDalFaker()
        {
            _users = new Dictionary<string, User>();
            employees = new Dictionary<string, User>();
            customers = new Dictionary<string, User>();
            _users.Add("Sol@gmail.com", new Employee("Sol", "Nguyen", "Eindhoven", 0987654321, new Account("Sol@gmail.com", "1234")));
        }

        public bool CreateUser(User user)
        {
            if (CheckUserEmailUnit(user.Account.Email) == null)
            {
                _users.Add(user.Account.Email, user);
                return true;
            }

            return false;

        }

        public bool AssignEmployee(User user)
        {


            if (user is Employee)
            {
                employees.Add(user.Account.Email, user);
                return true;
            }

            return false;
        }

        public bool AssignCustomer(User user)
        {

            if (user is Customer)
            {
                customers.Add(user.Account.Email, user);
                return true;
            }

            return false;
        }

        public User GetEmployeeByEmail(string email)
        {

            return employees.ContainsKey(email) ? employees[email] : null;

        }

        public User GetCustomerByEmail(string email)
        {
            return customers.ContainsKey(email) ? customers[email] : null;
        }


        public bool UpdateUser(User user)
        {
            user = _users[user.Account.Email];
            return true;

        }

        public User CheckUserEmailUnit(string email)
        {
            return _users.ContainsKey(email) ? _users[email] : null;
        }

        //public bool UserExist(string email)
        //{
        //    return _users.ContainsKey(email) ? _users[email] != null : false;
        //}
    }
}





