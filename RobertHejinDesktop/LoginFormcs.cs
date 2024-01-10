using BusinessLogic.ClassManagers;
using BusinessLogic.DAL.InterfacesDAL;
using BusinessLogic.InterfacesDAL;
using DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogic.Enum;
using BusinessLogic.ObjectClasses;

namespace RobertHejinDesktop
{
    public partial class LoginFormcs : Form
    {
        private readonly UserManager _userManager;

        public LoginFormcs()
        {
            InitializeComponent();
            IUserDal userDal = new UserDal();
            IAccountDal accountDal = new AccountDal();
            _userManager = new UserManager(userDal, accountDal);

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (_userManager.IsValidEmail(tbEmailLogin.Text) && !string.IsNullOrEmpty(tbPasswordLogin.Text))
            {
                User? user = _userManager.GetEmployeeByEmail(tbEmailLogin.Text);
                if (user != null)
                {
                    if (_userManager.CheckPassword(tbPasswordLogin.Text, user.Account.Password))
                    {
                        tbEmailLogin.Text = tbPasswordLogin.Text = string.Empty;
                        RobertHejinDesktop robertHejinDesktop = new RobertHejinDesktop(user);
                        Hide();
                        robertHejinDesktop.ShowDialog();
                        Show();
                    }
                    else
                    {
                        MessageBox.Show(@"Please check the password!");
                    }
                }
                else
                {
                    MessageBox.Show(@"There is no employee account matching this email");
                }
            }
            else
            {
                MessageBox.Show(@"Please fill in all fields with all valid data such as Email!");
            }
        }
    }
}