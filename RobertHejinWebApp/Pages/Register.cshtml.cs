using BusinessLogic.ClassManagers;
using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.Enum;
using BusinessLogic.InterfacesDAL;
using BusinessLogic.ObjectClasses;
using DataLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RobertHejinWebApp.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }
        private readonly UserManager _userManager;
        private readonly CartManager _cartManager;
        public string ResultMessage { get; set; }

        public RegisterModel()
        {
            IUserDal userDal = new UserDal();
            IAccountDal accountDal = new AccountDal();
            ICartDal cartDal = new CartDal();
            _userManager = new UserManager(userDal, accountDal);
            _cartManager = new CartManager(cartDal);
        }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {

            if (ModelState.IsValid)
            {

                User? user = _userManager.CheckUserEmailUnit(User.Account.Email);
                if (user == null)
                {
                    User.Account.Password = _userManager.HashPassword(User.Account.Password);
                    _userManager.CreateUser(User);
                    _userManager.AssignCustomer(User);
                    _cartManager.CreateCart(User);
                    ResultMessage = "User created successfully!";
                    return Page();
                }
                else
                {
                    ResultMessage = "Email already exist!";
                }
            }

            return Page();
        }
    }

}
