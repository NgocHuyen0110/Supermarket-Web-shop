using System.Drawing;
using System.Security.Claims;
using BusinessLogic.ClassManagers;
using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.Enum;
using BusinessLogic.InterfacesDAL;
using BusinessLogic.ObjectClasses;
using DataLayer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RobertHejinWebApp.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public string ErrorMessage { get; set; }


        private readonly UserManager _userManager;
        public LoginModel()
        {
            IUserDal userDal = new UserDal();
            IAccountDal accountDal = new AccountDal();
            _userManager = new UserManager(userDal, accountDal);
        }


        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            if (@ModelState.IsValid)
            {

                User? user = _userManager.GetCustomerByEmail(Email);
                if (user != null)
                {
                    if (_userManager.CheckPassword(Password, user.Account.Password))
                    {

                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.Account.Email));
                        claims.Add(new Claim("id", user.Id.ToString()));

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        ErrorMessage = "Please check the password!";
                    }
                }
                else
                {
                    ErrorMessage = "There is no customer account matching this email";
                }

            }

            return Page();
        }
    }
}
