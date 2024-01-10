using BusinessLogic.ClassManagers;
using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.InterfacesDAL;
using BusinessLogic.ObjectClasses;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RobertHejinWebApp.Pages
{
    public class ShoppingCartModel : PageModel
    {
        private readonly CartManager _cartManager;
        private readonly UserManager _userManager;
        private readonly ItemManager _itemManager;

        public Cart? Cart { get; set; }
        public User? UserLogin { get; set; }
        public Item Item { get; set; }
        public List<CartItem> CartItems { get; set; }
        public string Message { get; set; }

        public ShoppingCartModel()
        {
            ICartDal cartDal = new CartDal();
            _cartManager = new CartManager(cartDal);
            IUserDal userDal = new UserDal();
            IAccountDal accountDal = new AccountDal();
            IItemDal itemDal = new ItemDal();
            _userManager = new UserManager(userDal, accountDal);
            _itemManager = new ItemManager(itemDal);
        }

        public void OnGet()
        {
            UserLogin = _userManager.GetCustomerByEmail(User.Identity.Name);
            Cart = new Cart();
            Cart.CartItems = new List<CartItem>();
            Cart.CartItems = _cartManager.GetCartItems(UserLogin, Cart);
            Cart.TotalPrice = _cartManager.GetTotalPrice(UserLogin, Cart);
            //CartItems = _cartManager.GetCartItems(UserLogin, Cart);

        }

        public IActionResult OnPostDelete()
        {
            UserLogin = _userManager.GetCustomerByEmail(User.Identity.Name);
            Item = _itemManager.GetItemByName(Request.Form["ItemName"]);
            CartItem cartItem = _cartManager.CheckCartItemExist(UserLogin, Item);
            _cartManager.DeleteCartItem(UserLogin, cartItem);
            //CartItems = _cartManager.GetCartItems(UserLogin, Cart);
            return Redirect("/ShoppingCart");
        }

        //public void OnPostCheckOut()
        //{
        //    Cart = new Cart();
        //    Cart.CartItems = new List<CartItem>();
        //    Cart.CartItems = _cartManager.GetCartItems(UserLogin, Cart);
        //    foreach (var c in Cart.CartItems)
        //    {
        //        if (c.Quantity < c.Item.AmountInStock)
        //        {
        //            Redirect("/Order");

        //        }

        //        Message = $"{c.Item.ItemName} is not enough in stock, the amount in stock is {c.Item.AmountInStock}";

        //    }

        //    Page();
        //}
    }
}
