using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogic.ClassManagers;
using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.Enum;
using BusinessLogic.Interfaces.Interafce;
using BusinessLogic.InterfacesDAL;
using BusinessLogic.ObjectClasses;
using DataLayer;

namespace RobertHejinDesktop
{
    public partial class RobertHejinDesktop : Form
    {
        private readonly UserManager _userManager;
        private readonly CategoryManager _categoryManager;
        private readonly SubcategoryManager _subcategoryManager;
        private readonly ItemManager _itemManager;
        private readonly OrderManager _orderManager;
        private User _userLogin;
        private Item _item;
        private Order selectedOrder;


        public RobertHejinDesktop(User user)
        {
            InitializeComponent();
            IUserDal userDal = new UserDal();
            IAccountDal accountDal = new AccountDal();
            ICategoryDal categoryDal = new CategoryDal();
            ISubcategoryDal subcategoryDal = new SubcategoryDal();
            IItemDal itemDal = new ItemDal();
            _userManager = new UserManager(userDal, accountDal);
            _categoryManager = new CategoryManager(categoryDal);
            _subcategoryManager = new SubcategoryManager(subcategoryDal);
            _itemManager = new ItemManager(itemDal);
            IOrderDAL orderDal = new OrderDAL();
            _orderManager = new OrderManager(orderDal);
            _userLogin = user;

        }

        private void RobertHejinDesktop_Load(object sender, EventArgs e)
        {
            lblShowFirstName.Text = _userLogin.FirstName;
            lblShowLastName.Text = _userLogin.LastName;
            lblShowEmail.Text = _userLogin.Account.Email;
            tbChangeAddress.Text = _userLogin.Address;
            tbChangePhoneNr.Text = Convert.ToString(_userLogin.PhoneNr);
            cbbCategory.DataSource = _categoryManager.GetCategories();
            cbbSubcategory.Visible = false;
            lblSubcategory.Visible = false;
            cbbViewItemByCategory.DataSource = _categoryManager.GetCategories();
            gbItemInfor.Visible = false;
            groupBox1.Visible = false;

        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {

            foreach (Control control in createAccountTab.Controls)
            {
                if (control is not TextBox) continue;
                if (!string.IsNullOrEmpty(control.Text) && _userManager.IsValidEmail(tbEmail.Text))
                {
                    if (_userManager.CheckUserEmailUnit(tbEmail.Text) == null)
                    {
                        tbPassword.Text = _userManager.HashPassword(tbPassword.Text);
                        User user = new Employee(tbFirstName.Text, tbLastName.Text, tbAddress.Text,
                            Convert.ToInt32(tbPhone.Text), new Account(tbEmail.Text, tbPassword.Text));
                        MessageBox.Show(_userManager.CreateUser(user) ? "User created" : "User not created");
                        _userManager.AssignEmployee(user);
                        ClearCreateAccount();
                    }
                    else
                    {
                        MessageBox.Show(@"Email already exists");
                    }

                }
                else
                {
                    MessageBox.Show(@"Please fill in all fields with all valid data such as Email!");
                }

                break;

            }

        }

        private void ClearCreateAccount()
        {
            tbFirstName.Text = tbLastName.Text = tbAddress.Text = tbPhone.Text = tbEmail.Text = tbPassword.Text = "";
        }

        private void BtnUpdateProfile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbChangePassword.Text) || string.IsNullOrEmpty(tbChangeAddress.Text) ||
                string.IsNullOrEmpty(tbChangePhoneNr.Text))
            {
                MessageBox.Show(@"Please fill in all fields");
            }
            else
            {

                _userLogin.Account.Password = _userManager.HashPassword(tbChangePassword.Text);
                _userLogin.Address = tbChangeAddress.Text;
                _userLogin.PhoneNr = Convert.ToInt32(tbChangePhoneNr.Text);
                _userManager.UpdateUser(_userLogin);
                _userManager.UpdateAccount(_userLogin.Account);
                MessageBox.Show(@"Profile updated");
                tbChangePassword.Text = _userLogin.Account.Password;
            }
        }

        private void cbbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbSubcategory.DataSource = null;
            Category category = (Category)cbbCategory.SelectedItem;
            cbbSubcategory.Visible = true;
            lblSubcategory.Visible = true;
            cbbSubcategory.DataSource = _subcategoryManager.GetSubcategoriesByCategory(category);
        }

        private void btnCreateItem_Click(object sender, EventArgs e)
        {
            foreach (Control control in createItemTab.Controls)
            {
                if (control is not TextBox) continue;
                if (!string.IsNullOrEmpty(control.Text) && cbbCategory.SelectedItem != null &&
                    cbbSubcategory.SelectedItem != null)
                {
                    Item item = new Item(tbItemName.Text, Convert.ToDecimal(tbPrice.Text), tbUnit.Text, Convert.ToInt32(tbAmountInStock.Text), (Subcategory)cbbSubcategory.SelectedItem);
                    if (_itemManager.GetItemByName(tbItemName.Text) == null)
                    {
                        _itemManager.CreateItem(item);
                        MessageBox.Show(@"Item created");
                        ClearCreateItemForm();
                        dgvItem.Refresh();
                    }
                    else
                    {
                        MessageBox.Show(@"Item name already exists, Please choose a different name");
                    }


                }
                else
                {
                    MessageBox.Show(@"Please fill in all fields");
                }


                break;

            }
        }

        private void ClearCreateItemForm()
        {
            tbItemName.Text = tbPrice.Text = tbUnit.Text = tbAmountInStock.Text = "";
            cbbSubcategory.DataSource = null;
            cbbSubcategory.Visible = false;
            lblSubcategory.Visible = false;
            cbbCategory.ResetText();
        }

        private void cbbViewItemByCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvItem.DataSource = null;
            Category category = (Category)cbbViewItemByCategory.SelectedItem;
            dgvItem.DataSource = _itemManager.GetAllItemsByCategoryIncluOutOfStock(category);
            cbbViewItemByCategory.ResetText();
        }

        private void btnViewItemInfor_Click(object sender, EventArgs e)
        {
            gbItemInfor.Visible = true;
            _item = (Item)dgvItem.CurrentRow.DataBoundItem;
            lblItemNameInfor.Text = _item.ItemName;
            tbPriceInfor.Text = Convert.ToString(_item.Price);
            tbUnitInfor.Text = _item.Unit;
            lblSubInfor.Text = _item.Subcategory.SubcategoryItem;
            tbAmountInfor.Text = Convert.ToString(_item.AmountInStock);
        }


        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (dgvItem.SelectedRows.Count > 0)
            {
                _item = (Item)dgvItem.CurrentRow.DataBoundItem;
                _itemManager.DeleteItem(_item);
                MessageBox.Show(@"Item deleted");
                dgvItem.Refresh();
            }
            else
            {
                MessageBox.Show(@"Please select an item");
            }
        }

        private void btnUpdateItem_Click_1(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbPriceInfor.Text) && !String.IsNullOrEmpty(tbUnitInfor.Text) &&
                !String.IsNullOrEmpty(tbAmountInfor.Text))
            {
                _item.Price = Convert.ToDecimal(tbPriceInfor.Text);
                _item.Unit = tbUnitInfor.Text;
                _item.AmountInStock = Convert.ToInt32(tbAmountInfor.Text);
                _itemManager.UpdateItem(_item);
                MessageBox.Show(@"Item updated");
                dgvItem.Refresh();
            }
            else
            {
                MessageBox.Show(@"Please fill in all fields");
            }
        }

        private void btnCloseItem_Click_1(object sender, EventArgs e)
        {
            gbItemInfor.Visible = false;
            gbItemInfor.ResetText();
        }

        private void rdbHome_CheckedChanged_1(object sender, EventArgs e)
        {
            dgvItem.DataSource = null;
            if (rdbHome.Checked)
            {
                dgvOrders.DataSource = _orderManager.HomeDeliveryOrders();
            }
        }

        private void rdbPickUp_CheckedChanged(object sender, EventArgs e)
        {
            dgvItem.DataSource = null;
            if (rdbPickUp.Checked)
            {
                dgvOrders.DataSource = _orderManager.PickDeliveryOrders();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rdbAccepted.Checked || rdbPreparing.Checked || rdbShipping.Checked)
            {

                if (rdbAccepted.Checked)
                {
                    selectedOrder.OrderStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), "Accepted");
                }
                else if (rdbPreparing.Checked)
                {
                    selectedOrder.OrderStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), "Preparing");
                }
                else
                {
                    selectedOrder.OrderStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), "Shipping");
                }
                _orderManager.UpdateOrder(selectedOrder);
                MessageBox.Show(@"Order updated");
                dgvOrders.Refresh();
            }
            else
            {
                MessageBox.Show(@"Please select an order status");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            selectedOrder = (Order)dgvOrders.CurrentRow.DataBoundItem;
            groupBox1.Visible = true;
            label13.Text = selectedOrder.Id.ToString();
            label12.Text = selectedOrder.OrderDate.ToString();
            label11.Text = selectedOrder.OrderDate.ToShortDateString();
            label20.Text = selectedOrder.DeliverOption.Address.Address.ToString();
            label23.Text = selectedOrder.DeliverOption.DeliveryDate.ToShortDateString() + " " + selectedOrder.DeliverOption.TimeSlot.ToString();
        }
    }
}