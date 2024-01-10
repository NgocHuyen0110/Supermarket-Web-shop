using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.ObjectClasses;

namespace BusinessLogic.ClassManagers
{
    public class ItemManager
    {
        private readonly IItemDal _itemDal;

        public ItemManager(IItemDal itemDal)
        {
            this._itemDal = itemDal;
        }
        public bool CreateItem(Item item)
        {
            return _itemDal.CreateItem(item);

        }
        public bool DeleteItem(Item item)
        {
            return _itemDal.DeleteItem(item);
        }
        public bool UpdateItem(Item item)
        {

            return _itemDal.UpdateItem(item);
        }
        public List<Item> GetAllItemsByCategory(Category category)
        {
            return _itemDal.GetAllItemsByCategory(category);
        }
        public Item GetItemByName(string itemName)
        {
            return _itemDal.GetItemByName(itemName);
        }
        public List<Item> GetAllItems()
        {
            return _itemDal.GetAllItems();
        }
        public bool UpdateAmountInStock(Item item, CartItem cartItem)
        {
            item.AmountInStock -= cartItem.Quantity;
            return _itemDal.UpdateAmountInStock(item);
        }
        public List<Item> GetAllItemsByCategoryIncluOutOfStock(Category category)
        {
            return _itemDal.GetAllItemsByCategoryIncluOutOfStock(category);
        }
    }
}
