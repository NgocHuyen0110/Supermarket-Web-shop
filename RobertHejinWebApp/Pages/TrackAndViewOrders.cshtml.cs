using BusinessLogic.ClassManagers;
using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.InterfacesDAL;
using BusinessLogic.ObjectClasses;
using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RobertHejinWebApp.Pages
{
    [Authorize]
    public class TrackAndViewOrdersModel : PageModel
    {
        private readonly UserManager _userManager;
        private readonly OrderManager _orderManager;
        public List<Order> HomeDeliveryOrders { get; set; }
        public List<Order> PickUpOrders { get; set; }

        public User? UserLogin { get; set; }
        public TrackAndViewOrdersModel()
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
            HomeDeliveryOrders = _orderManager.HistoryHomeDeliveryOrders(UserLogin);
            foreach (var o in HomeDeliveryOrders)
            {
                o.OrderItems = _orderManager.GetOrderItems(o);
            }
            PickUpOrders = _orderManager.HistoryPickDeliveryOrders(UserLogin);
            foreach (var o in PickUpOrders)
            {
                o.OrderItems = _orderManager.GetOrderItems(o);
            }
        }
    }
}
