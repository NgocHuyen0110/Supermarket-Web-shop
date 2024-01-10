using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.ObjectClasses;

namespace UnitTestRoberHejin.FakeData
{
    public class AccountDalFaker : IAccountDal
    {
        private Dictionary<string, Account> _accounts;

        public AccountDalFaker()
        {
            _accounts = new Dictionary<string, Account>();
        }
        public bool CreateAccount(Account account)
        {
            if (!AccountExist(account.Email))
            {
                _accounts.Add(account.Email, account);
                return true;
            }
            return false;
        }

        public bool UpdateAccount(Account account)
        {
            _accounts[account.Email] = account;
            return true;
        }
        public bool AccountExist(string email)
        {
            return _accounts.ContainsKey(email);
        }
    }

}
