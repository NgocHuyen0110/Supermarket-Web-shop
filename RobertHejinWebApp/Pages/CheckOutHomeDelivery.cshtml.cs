using BusinessLogic.ClassManagers;
using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.Enum;
using BusinessLogic.Interfaces.Interafce;
using BusinessLogic.InterfacesDAL;
using BusinessLogic.ObjectClasses;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RobertHejinWebApp.Pages
{
    public class CheckOutHomeDeliveryModel : PageModel
    {
        private readonly IHomeDelivery _homeDelivery;
        public Dictionary<TimeSpan, TimeSlot> HomeDeliveryTimeSlots { get; set; }
        [BindProperty]
        public DeliveryOption DeliveryOption { get; set; }
        //[BindProperty]
        //public DeliveryAddress DeliveryAddress { get; set; }
        //[BindProperty]
        //public TimeOnly TimeSlot { get; set; }
        //[BindProperty]
        //public DateOnly DeliveryDate { get; set; }
        [BindProperty]
        public User? UserLogin { get; set; }
        [BindProperty]
        public DeliveryAddress DeliveryAddress { get; set; }
        [BindProperty]
        public TimeSlot TimeSlot { get; set; }
        [BindProperty]
        public DateTime DeliveryDate { get; set; }
        private readonly UserManager _userManager;
        private readonly CartManager _cartManager;
        private readonly OrderManager _orderManager;
        private readonly ItemManager _itemManager;
        [BindProperty]
        public Cart? Cart { get; set; }
        [BindProperty]
        public Order Order { get; set; }
        public CheckOutHomeDeliveryModel()
        {
            IDeliveryDAL deliveryDal = new DeliveryDAL();
            _homeDelivery = new DeliveryManager(deliveryDal);
            IUserDal userDal = new UserDal();
            IAccountDal accountDal = new AccountDal();
            _userManager = new UserManager(userDal, accountDal);
            IOrderDAL orderDal = new OrderDAL();
            _orderManager = new OrderManager(orderDal);
            ICartDal cartDal = new CartDal();
            _cartManager = new CartManager(cartDal);
            IItemDal itemDal = new ItemDal();
            _itemManager = new ItemManager(itemDal);
        }
        public void OnGet()
        {
            DeliveryOption = new HomeDelivery();
            DeliveryOption.DeliveryDate = (DateTime.Today).AddDays(1);
            UserLogin = _userManager.GetCustomerByEmail(User.Identity.Name);
            //HomeDeliveryTimeSlots = _homeDelivery.GetTimeSlot(DeliveryOption);
            //DeliveryDate = (DateTime.Now).Date.AddDays(1);

            HomeDeliveryTimeSlots = new Dictionary<TimeSpan, TimeSlot>();
            foreach (var timeSlot in _homeDelivery.GetTimeSlotsHomeDelivery().Keys)

            {
                DeliveryOption.TimeSlot = new TimeSlot(timeSlot);
                if (_homeDelivery.CountTimeSlot(DeliveryOption) < 5)
                {
                    HomeDeliveryTimeSlots.Add(timeSlot, new TimeSlot(timeSlot));
                }
            }
            Cart = new Cart();
            Cart.CartItems = new List<CartItem>();
            Cart.CartItems = _cartManager.GetCartItems(UserLogin, Cart);
            Cart.TotalPrice = _cartManager.GetTotalPrice(UserLogin, Cart);

        }

        public IActionResult OnPost()
        {

            DeliveryOption = new HomeDelivery(TimeSlot, DeliveryDate, DeliveryAddress);
            UserLogin = _userManager.GetCustomerByEmail(User.Identity.Name);
            // Order.OrderDate = (DateTime.Now).Date;
            Order.OrderDate = (DateTime.Today).AddDays(1);
            Order.User = UserLogin;
            Order.OrderStatus = (OrderStatus)1;
            Order.DeliverOption = DeliveryOption;
            Order.OrderItems = new List<CartItem>();
            Cart = new Cart();
            Cart.CartItems = new List<CartItem>();
            Cart.CartItems = _cartManager.GetCartItems(UserLogin, Cart);
            Order.OrderItems = Cart.CartItems;
            Order.TotalPrice = _cartManager.GetTotalPrice(UserLogin, Cart);
            if (_homeDelivery.GetHomeDeliveryAddress(DeliveryAddress) == null)
            {
                _homeDelivery.CreateHomeDeliveryAddress(DeliveryOption);
            }
            _orderManager.CreateOrder(Order);
            _orderManager.CreateOrderItems(Order);
            foreach (var c in Order.OrderItems)
            {
                _itemManager.UpdateAmountInStock(c.Item, c);

                _cartManager.DeleteCartItem(UserLogin, c);

            }

            return Redirect("/CompletedOrder");


        }

    }
}
