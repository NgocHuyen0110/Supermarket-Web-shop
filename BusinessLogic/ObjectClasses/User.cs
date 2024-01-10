using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Enum;

namespace BusinessLogic.ObjectClasses
{
    public class User
    {
        public int Id { get; private set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int PhoneNr { get; set; }
        [Required]
        public Account Account { get; set; }
        public User(string firstName, string lastName, string address, int phone, Account account)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            PhoneNr = phone;
            Account = account;
        }
        public User(int id, string firstName, string lastName, string address, int phone, Account account)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            PhoneNr = phone;
            Account = account;
        }


        public User()
        {

        }

    }
}
