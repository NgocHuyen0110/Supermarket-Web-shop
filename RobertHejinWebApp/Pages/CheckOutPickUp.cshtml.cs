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
    public class CheckOutPickUpModel : PageModel
    {
        private readonly IPickUp _pickUp;
        public List<DeliveryAddress> Addresses { get; set; }
        private readonly UserManager _userManager;
        public User? UserLogin { get; set; }
        [BindProperty]
        public DeliveryAddress PickUpAddress { get; set; }
        [BindProperty]
        public TimeSlot TimeSlot { get; set; }
        [BindProperty]
        public DateTime DeliveryDate { get; set; }
        [BindProperty]
        public DeliveryOption DeliveryOption { get; set; }
        public Dictionary<TimeSpan, TimeSlot> PickUpTimeSlots { get; set; }
        [BindProperty]
        public Cart? Cart { get; set; }
        [BindProperty]
        public Order Order { get; set; }
        private readonly CartManager _cartManager;
        private readonly OrderManager _orderManager;
        private readonly ItemManager _itemManager;

        public CheckOutPickUpModel()
        {
            IDeliveryDAL deliveryDal = new DeliveryDAL();
            _pickUp = new DeliveryManager(deliveryDal);
            IUserDal userDal = new UserDal();
            IAccountDal accountDal = new AccountDal();
            _userManager = new UserManager(userDal, accountDal);
            PickUpTimeSlots = new Dictionary<TimeSpan, TimeSlot>();
            IOrderDAL orderDal = new OrderDAL();
            _orderManager = new OrderManager(orderDal);
            ICartDal cartDal = new CartDal();
            _cartManager = new CartManager(cartDal);
            IItemDal itemDal = new ItemDal();
            _itemManager = new ItemManager(itemDal);
            PickUpAddress = new DeliveryAddress();
        }
        public void OnGet()
        {
            Addresses = _pickUp.GetAllAddress();
            UserLogin = _userManager.GetCustomerByEmail(User.Identity.Name);
            DeliveryDate = (DateTime.Today).AddDays(1);
            PickUpAddress = new DeliveryAddress();
        }
        public void OnPostAddress()
        {
            Addresses = _pickUp.GetAllAddress();
            UserLogin = _userManager.GetCustomerByEmail(User.Identity.Name);
            DeliveryDate = (DateTime.Today).AddDays(1);
            PickUpTimeSlots = new Dictionary<TimeSpan, TimeSlot>();
            foreach (var timeSlot in _pickUp.GetTimeSlotsPickUp().Keys)

            {
                TimeSlot = new TimeSlot(timeSlot);
                DeliveryOption = new PickUp(TimeSlot, DeliveryDate, PickUpAddress);

                if (_pickUp.CountTimeSlot(DeliveryOption) < 2)
                {
                    PickUpTimeSlots.Add(timeSlot, new TimeSlot(timeSlot));
                }
            }
        }
        public IActionResult OnPostPlaceOrder()
        {

            DeliveryOption = new PickUp(TimeSlot, DeliveryDate, PickUpAddress);
            UserLogin = _userManager.GetCustomerByEmail(User.Identity.Name);
            Order.OrderDate = (DateTime.Now).Date;
            Order.User = UserLogin;
            Order.OrderStatus = (OrderStatus)1;
            Order.DeliverOption = DeliveryOption;
            Order.OrderItems = new List<CartItem>();
            Cart = new Cart();
            Cart.CartItems = new List<CartItem>();
            Cart.CartItems = _cartManager.GetCartItems(UserLogin, Cart);
            Order.OrderItems = Cart.CartItems;
            Order.TotalPrice = _cartManager.GetTotalPrice(UserLogin, Cart);
            _orderManager.CreateOrder(Order);
            _orderManager.CreateOrderItems(Order);
            foreach (var c in Order.OrderItems)
            {

                _itemManager.UpdateAmountInStock(c.Item, c);
                _cartManager.DeleteCartItem(UserLogin, c);
            }
            return Redirect("/CompletedPickUpOrder");

        }
    }
}
