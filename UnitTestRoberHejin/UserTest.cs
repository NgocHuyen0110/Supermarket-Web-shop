using BusinessLogic.ClassManagers;
using BusinessLogic.Enum;
using BusinessLogic.InterfacesDAL;
using BusinessLogic.ObjectClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestRoberHejin.FakeData;

namespace UnitTestRoberHejin
{
    [TestClass]
    public class UserTest
    {
        private UserManager _userManager = new UserManager(new UserDalFaker(), new AccountDalFaker());

        public UserTest()
        {

        }

        [TestMethod]
        public void CreateUser()
        {
            User user1 = new Employee("Roxana", "Hejin", "Hovedgaden 1", 12345678, new Account("Roxana@gmail.com", "1234"));
            bool result1 = _userManager.CreateUser(user1);
            Assert.IsTrue(result1);

        }
        [TestMethod]
        public void CreateUserWithExistEmail()
        {
            User user2 = new Employee("John", "Doe", "Hovedgaden 2", 12345678, new Account("Sol@gmail.com", "1234"));
            bool result2 = _userManager.CreateUser(user2);
            Assert.IsFalse(result2);
        }

        [TestMethod]
        public void AssginEmployee()
        {

            User user1 = new Employee("Roxana", "Hejin", "Hovedgaden 1", 12345678, new Account("Roxana@gmail.com", "1234"));
            _userManager.CreateUser(user1);
            bool result1 = _userManager.AssignEmployee(user1);
            Assert.IsTrue(result1);
        }
        [TestMethod]
        public void AssginCustomer()
        {

            User user1 = new Employee("Roxana", "Hejin", "Hovedgaden 1", 12345678, new Account("Roxana@gmail.com", "1234"));
            _userManager.CreateUser(user1);
            bool result1 = _userManager.AssignCustomer(user1);
            Assert.IsFalse(result1);
        }

        [TestMethod]
        public void GetEmployeeByEmail()
        {
            User user1 = new Employee("Roxana", "Hejin", "Hovedgaden 1", 12345678, new Account("Roxana@gmail.com", "1234"));
            _userManager.CreateUser(user1);
            _userManager.AssignEmployee(user1);
            User user = _userManager.GetEmployeeByEmail("Roxana@gmail.com");
            Assert.AreEqual(user1, user);

        }
        [TestMethod]
        public void GetCustomerByEmailWithNoEmailExist()
        {
            User user1 = new Employee("Roxana", "Hejin", "Hovedgaden 1", 12345678, new Account("Roxana@gmail.com", "1234"));
            _userManager.CreateUser(user1);
            _userManager.AssignCustomer(user1);
            User user = _userManager.GetEmployeeByEmail("Roxana@gmail.com");
            Assert.AreEqual(null, user);

        }

        [TestMethod]
        public void UpdateUser()
        {
            User user1 = new User("Roxana", "Hejin", "Hovedgaden 1", 12345678, new Account("Roxana@gmail.com", "1234"));
            _userManager.CreateUser(user1);
            user1.Address = "Hovedgaden 2";
            user1.PhoneNr = 87654321;
            user1.Account.Password = "4321";
            bool result = _userManager.UpdateUser(user1);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void CreateAccount()
        {
            Account account1 = new Account("thai@gmail.com", "1234");
            bool result1 = _userManager.CreateAccount(account1);
            Assert.IsTrue(result1);
        }
        [TestMethod]
        public void CreateAccountWithEmailExist()
        {
            Account account1 = new Account("Sol@gmail.com", "333");
            _userManager.CreateAccount(account1);
            Account account2 = new Account("Sol@gmail.com", "333");
            bool result2 = _userManager.CreateAccount(account2);
            Assert.IsFalse(result2);


        }
        [TestMethod]
        public void UpdateAccount()
        {

            Account account1 = new Account("thai@gmail.com", "1234");
            account1.Password = "4321";
            _userManager.UpdateAccount(account1);

        }


    }
}