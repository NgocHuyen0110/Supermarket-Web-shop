using BusinessLogic.ClassManagers;
using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.InterfacesDAL;
using BusinessLogic.ObjectClasses;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RobertHejinWebApp.Pages
{
    public class CompletedOrderModel : PageModel
    {
        private readonly UserManager _userManager;
        private readonly OrderManager _orderManager;
        public User? UserLogin { get; set; }
        public Order? Order { get; set; }
        public List<CartItem>? CartItems { get; set; }

        public CompletedOrderModel()
        {
            IUserDal userDal = new UserDal();
            IAccountDal accountDal = new AccountDal();
            _userManager = new UserManager(userDal, accountDal);
            IOrderDAL orderDal = new OrderDAL();
            _orderManager = new OrderManager(orderDal);
        }
        public void OnGet()
        {
            UserLogin = _userManager.GetCustomerByEmail(User.Identity.Name);
            Order = _orderManager.GetLastHomeOrderByUser(UserLogin);
            Order.OrderItems = _orderManager.GetOrderItems(Order);
        }
    }

}
