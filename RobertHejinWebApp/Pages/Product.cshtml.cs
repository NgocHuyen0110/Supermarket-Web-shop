using System.ComponentModel.DataAnnotations;
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
    public class ProductModel : PageModel
    {
        private readonly ItemManager _itemManager;
        //public CartItem CartItem { get; set; }
        public List<Item> Items { get; set; }
        private readonly UserManager _userManager;
        private readonly CartManager _cartManager;
        private readonly CategoryManager _categoryManager;
        public Item Item { get; set; }
        public User? UserLogin { get; set; }
        public string Name { get; set; }
        [BindProperty]
        public int Quantity { get; set; }
        public List<Category> Categories { get; set; }
        [BindProperty]
        public Category Category { get; set; }

        public ProductModel()
        {
            IItemDal itemDal = new ItemDal();
            _itemManager = new ItemManager(itemDal);
            IUserDal userDal = new UserDal();
            IAccountDal accountDal = new AccountDal();
            ICartDal cartDal = new CartDal();
            _userManager = new UserManager(userDal, accountDal);
            _cartManager = new CartManager(cartDal);
            ICategoryDal categoryDal = new CategoryDal();
            _categoryManager = new CategoryManager(categoryDal);
            Category = new Category();
            Category.Subcatergories = new List<Subcategory>();
        }

        public void OnGet()
        {
            Items = _itemManager.GetAllItems();
            Categories = _categoryManager.GetCategories();
        }

        public IActionResult OnPostAddItem()
        {
            if (ModelState.IsValid)
            {
                Items = _itemManager.GetAllItems();
                Categories = _categoryManager.GetCategories();
                Item = _itemManager.GetItemByName(Request.Form["itemName"]);
                UserLogin = _userManager.GetCustomerByEmail(User.Identity.Name);
                Cart cart = new Cart();
                CartItem cartItem = new CartItem(Quantity);
                _cartManager.AddItem(cartItem, UserLogin, cart, Item);

            }
            return Redirect("/Product");
        }


        public void OnPostCategory()
        {
            Items = _itemManager.GetAllItemsByCategory(Category);
            Categories = _categoryManager.GetCategories();
        }
    }

}
