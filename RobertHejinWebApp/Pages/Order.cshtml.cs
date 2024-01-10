using BusinessLogic.ClassManagers;
using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.Interfaces.Interafce;
using BusinessLogic.InterfacesDAL;
using BusinessLogic.ObjectClasses;
using DataLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.SqlServer.Management.HadrModel;

namespace RobertHejinWebApp.Pages
{
    public class OrderModel : PageModel
    {
        [BindProperty]
        public string Delivery { get; set; }

        public string[] Deliveries = new[] { "Home Delivery", "Pick Up" };


        private readonly CartManager _cartManager;
        private readonly UserManager _userManager;
        //private readonly IHomeDelivery _homeDelivery;
        //public Dictionary<TimeOnly, TimeSlot> timeSlots = new Dictionary<TimeOnly, TimeSlot>();
        public Cart? Cart { get; set; }
        public User? UserLogin { get; set; }
        //[BindProperty]
        //public DeliveryOption DeliveryOption { get; set; }

        public OrderModel()
        {
            ICartDal cartDal = new CartDal();
            _cartManager = new CartManager(cartDal);
            IUserDal userDal = new UserDal();
            IAccountDal accountDal = new AccountDal();
            _userManager = new UserManager(userDal, accountDal);
            IDeliveryDAL deliveryDal = new DeliveryDAL();
            //_homeDelivery = new DeliveryManager(deliveryDal);


        }
        public void OnGet()
        {
            UserLogin = _userManager.GetCustomerByEmail(User.Identity.Name);
            Cart = new Cart();
            Cart.CartItems = new List<CartItem>();
            Cart.CartItems = _cartManager.GetCartItems(UserLogin, Cart);
            Cart.TotalPrice = _cartManager.GetTotalPrice(UserLogin, Cart);
        }


        public IActionResult OnPost()
        {
            if (Delivery == "Home Delivery")
            {
                return RedirectToPage("CheckOutHomeDelivery");
            }
            else
            {
                return RedirectToPage("CheckOutPickUp");
            }
        }
    }
}
