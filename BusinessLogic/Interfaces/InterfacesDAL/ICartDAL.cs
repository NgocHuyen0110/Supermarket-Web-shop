using BusinessLogic.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DAL.InterfacesDAL
{
    public interface ICartDal
    {
        bool CreateCart(User user);
        List<CartItem> GetCartItems(User user, Cart cart);
        bool AddItem(Item item, User user, CartItem cartItem);
        bool UpdateCart(User user, Item item, CartItem cartItem);
        bool DeleteCartItem(User user, CartItem cartItem);
        CartItem CheckCartItemExist(User user, Item item);
    }
}
