using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.ObjectClasses;

namespace BusinessLogic.ClassManagers
{
    public class CartManager
    {
        private readonly ICartDal _cartDal;
        public CartManager(ICartDal cartDal)
        {
            this._cartDal = cartDal;
        }

        public bool CreateCart(User user)
        {
            return _cartDal.CreateCart(user);
        }
        public List<CartItem> GetCartItems(User user, Cart cart)
        {
            return _cartDal.GetCartItems(user, cart);
        }
        public bool AddItem(CartItem cartItem, User user, Cart cart, Item item1)
        {
            if (GetCartItems(user, cart) == null)
            {
                return _cartDal.AddItem(item1, user, cartItem);
            }
            else
            {
                if (CheckCartItemExist(user, item1) != null)
                {
                    CartItem cartItem1 = CheckCartItemExist(user, item1);
                    cartItem1.Quantity += cartItem.Quantity;
                    return UpdateCart(user, item1, cartItem1);
                }
            }
            return _cartDal.AddItem(item1, user, cartItem);


        }
        public bool UpdateCart(User user, Item item, CartItem cartItem)
        {
            return _cartDal.UpdateCart(user, item, cartItem);
        }
        public bool DeleteCartItem(User user, CartItem cartItem)
        {
            return _cartDal.DeleteCartItem(user, cartItem);
        }
        public CartItem CheckCartItemExist(User user, Item item)
        {
            return _cartDal.CheckCartItemExist(user, item);
        }
        public decimal GetTotalPrice(User user, Cart cart)
        {
            foreach (CartItem cartItem in GetCartItems(user, cart))
            {
                cart.TotalPrice += cartItem.Item.Price * cartItem.Quantity;

            }
            return cart.TotalPrice;
        }
    }
}
