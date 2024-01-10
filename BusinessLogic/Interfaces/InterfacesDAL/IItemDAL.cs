using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.ObjectClasses;

namespace BusinessLogic.DAL.InterfacesDAL
{
    public interface IItemDal
    {
        bool CreateItem(Item item);
        bool DeleteItem(Item item);
        bool UpdateItem(Item item);
        List<Item> GetAllItemsByCategory(Category category);
        Item GetItemByName(string itemName);
        List<Item> GetAllItems();
        bool UpdateAmountInStock(Item item);
        List<Item> GetAllItemsByCategoryIncluOutOfStock(Category category);
    }
}
