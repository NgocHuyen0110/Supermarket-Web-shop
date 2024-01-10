using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ObjectClasses
{
    public class Employee : User
    {
        public Employee(string firstName, string lastName, string address, int phone, Account account) : base(firstName, lastName, address, phone, account)
        {
        }

        public Employee(int id, string firstName, string lastName, string address, int phone, Account account) : base(id, firstName, lastName, address, phone, account)
        {
        }

        public Employee()
        {
        }
    }

}
