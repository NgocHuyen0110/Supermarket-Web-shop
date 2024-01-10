using System.Drawing;
using BusinessLogic.ClassManagers;
using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.InterfacesDAL;
using BusinessLogic.ObjectClasses;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RobertHejinWebApp.Pages
{
    public class EditProfileModel : PageModel
    {
        private readonly UserManager _userManager;

        [BindProperty]
        public User? UserLogin { get; set; }


        public string ResultMessage { get; set; }
        public EditProfileModel()
        {
            IUserDal userDal = new UserDal();
            IAccountDal accountDal = new AccountDal();
            _userManager = new UserManager(userDal, accountDal);
        }
        public void OnGet()
        {
            UserLogin = _userManager.GetCustomerByEmail(User.Identity.Name);
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                User user1 = _userManager.GetCustomerByEmail(User.Identity.Name);
                user1.Account.Password = _userManager.HashPassword(UserLogin.Account.Password);
                user1.Address = UserLogin.Address;
                user1.PhoneNr = UserLogin.PhoneNr;


                _userManager.UpdateUser(user1);
                _userManager.UpdateAccount(user1.Account);
                ResultMessage = "Profile updated";
            }
            return Redirect("/EditProfile");
        }


    }
}
