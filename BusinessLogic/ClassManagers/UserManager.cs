using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.InterfacesDAL;
using BusinessLogic.ObjectClasses;
using Scrypt;

namespace BusinessLogic.ClassManagers
{
    public class UserManager
    {
        private readonly IUserDal _userDal;
        private readonly IAccountDal _accountDal;
        public UserManager(IUserDal userDal, IAccountDal accountDal)
        {
            this._userDal = userDal;
            this._accountDal = accountDal;
        }
        public User? GetEmployeeByEmail(string email)
        {
            return _userDal.GetEmployeeByEmail(email);
        }
        public User? GetCustomerByEmail(string email)
        {
            return _userDal.GetCustomerByEmail(email);
        }
        public bool CreateUser(User user)
        {
            if (_accountDal.CreateAccount(user.Account))
            {
                return _userDal.CreateUser(user);
            }
            return false;
        }
        public bool CreateAccount(Account account)
        {
            return _accountDal.CreateAccount(account);
        }
        public string HashPassword(string password)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            return encoder.Encode(password);

        }
        public bool CheckPassword(string password, string hash)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            return encoder.Compare(password, hash);
        }
        public bool UpdateUser(User user)
        {
            return _userDal.UpdateUser(user);
        }
        public bool UpdateAccount(Account account)
        {
            return _accountDal.UpdateAccount(account);
        }
        public bool AssignEmployee(User user)
        {
            return _userDal.AssignEmployee(user);
        }
        public bool AssignCustomer(User user)
        {
            return _userDal.AssignCustomer(user);
        }
        public User? CheckUserEmailUnit(string email)
        {
            return _userDal.CheckUserEmailUnit(email);
        }
        public bool IsValidEmail(string email)
        {

            Regex emailregex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase);
            return emailregex.IsMatch(email);
        }


    }
}
