using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrypt;

namespace BusinessLogic.ObjectClasses
{
    public class Account
    {
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public Account(string email, string password)
        {
            Email = email;
            Password = password;
        }
        public Account(string email)
        {

        }

        public Account()
        {

        }
    }
}
