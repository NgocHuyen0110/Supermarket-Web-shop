using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.ObjectClasses;

namespace UnitTestRoberHejin.FakeData
{
    public class ItemDalFaker : IItemDal
    {
        private Dictionary<string, Item> _items;

        public ItemDalFaker()
        {
            _items = new Dictionary<string, Item>();
            _items.Add("Mango", new Item("Mango", Convert.ToDecimal(2.5), "kilo", 100, new Subcategory("Fruit")));
        }
        public bool CreateItem(Item item)
        {
            if (!ItemExists(item.ItemName))
            {
                _items.Add(item.ItemName, item);
                return true;
            }

            return false;
        }

        public bool DeleteItem(Item item)
        {
            if (ItemExists(item.ItemName))
            {
                _items.Remove(item.ItemName);
                return true;
            }

            return false;
        }

        public bool UpdateItem(Item item)
        {
            if (!ItemExists(item.ItemName))
            {
                _items[item.ItemName] = item;
                return true;
            }

            return false;

        }

        public List<Item> GetAllItemsByCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Item GetItemByName(string name)
        {
            if (_items.ContainsKey(name))
            {
                return _items[name];
            }
            return null;
        }

        public List<Item> GetAllItems()
        {
            return _items.Values.ToList();
        }
        private bool ItemExists(string name)
        {
            return _items.ContainsKey(name);
        }

        public bool UpdateAmountInStock(Item item)
        {
            item = _items[item.ItemName];
            return true;
        }

        public List<Item> GetAllItemsByCategoryIncluOutOfStock(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
