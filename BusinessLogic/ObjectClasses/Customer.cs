using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ObjectClasses
{
    public class Customer : User
    {
        public Customer(string firstName, string lastName, string address, int phone, Account account) : base(firstName, lastName, address, phone, account)
        {
        }

        public Customer(int id, string firstName, string lastName, string address, int phone, Account account) : base(id, firstName, lastName, address, phone, account)
        {
        }

        public Customer()
        {
        }
        public override string ToString()
        {
            return FirstName.ToString() + " " + LastName.ToString();
        }
    }

}
