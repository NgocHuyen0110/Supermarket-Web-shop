using BusinessLogic.ClassManagers;
using BusinessLogic.Enum;
using BusinessLogic.InterfacesDAL;
using BusinessLogic.ObjectClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestRoberHejin.FakeData;

namespace UnitTestRoberHejin
{
    [TestClass]
    public class ItemTest
    {
        private ItemManager _itemManager = new ItemManager(new ItemDalFaker());
        public ItemTest()
        {

        }

        [TestMethod]
        public void CreateItem()
        {
            Item item = new Item("Banana", Convert.ToDecimal(1.44), "500g", 100, new Subcategory("Fruit"));
            bool result = _itemManager.CreateItem(item);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void CreateItemWithNameExist()
        {
            Item item = new Item("Mango", Convert.ToDecimal(2.2), "300g", 200, new Subcategory("Fruit"));
            bool result = _itemManager.CreateItem(item);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteItem()
        {
            Item item = new Item("Mango", Convert.ToDecimal(2.2), "300g", 200, new Subcategory("Fruit"));
            _itemManager.CreateItem(item);
            bool result = _itemManager.DeleteItem(item);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void DeleteItemNoExist()
        {
            Item item = new Item("Mango", Convert.ToDecimal(2.2), "300g", 200, new Subcategory("Fruit"));
            _itemManager.CreateItem(item);
            Item item1 = new Item("banana", Convert.ToDecimal(1.00), "kilo", 200, new Subcategory("Fruit"));
            bool result = _itemManager.DeleteItem(item1);
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void GetItemByName()
        {
            Item item = new Item("Orange", Convert.ToDecimal(2.2), "300g", 200, new Subcategory("Fruit"));
            _itemManager.CreateItem(item);
            Item item2 = _itemManager.GetItemByName("Orange");
            Assert.AreEqual(item, item2);
        }
        [TestMethod]
        public void GetItemByNameWithNoNameExist()
        {
            Item item = _itemManager.GetItemByName("Banana");
            Assert.AreEqual(null, item);
        }


    }
}