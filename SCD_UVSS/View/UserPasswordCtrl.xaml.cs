using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SCD_UVSS.Dal;
using SCD_UVSS.Model;

namespace SCD_UVSS.View
{
    /// <summary>
    /// Interaction logic for UserPasswordCtrl.xaml
    /// </summary>
    public partial class UserPasswordCtrl : UserControl
    {
        public readonly UserInfo UserInfo;

        public UserInfo LoggedInUser
        {
            get { return UserManager.Instance.LoggedInUser; }
        }

        public UserPasswordCtrl(UserInfo userInfo)
        {
            InitializeComponent();

            this.UserInfo = userInfo;
            
            this.UserNameTxt = userInfo.Name;
            this.PasswordTxt = userInfo.Password;
            this.RoleText = userInfo.Role.ToString();
            
            this.DisablePasswordForSuperIfNotDeveloper();
        }

        public string UserNameTxt
        {
            get { return this.txtUser.Text; }
            set { this.txtUser.Text = value; }
        }

        public string PasswordTxt
        {
            get { return this.txtPassword.Password; }
            set { this.txtPassword.Password = value; }
        }

        public string RoleText
        {
            get
            {
                return this.txtRole.Text;
            }
            set
            {
                this.txtRole.Text = value;
            }
        }

        private void DisablePasswordForSuperIfNotDeveloper()
        {
            if ((this.UserInfo.Role == Roles.Developer)
                && (this.LoggedInUser.Role != Roles.Developer))
            {
                this.txtUser.IsEnabled = false;
                this.txtPassword.IsEnabled = false;
            }
        }
    }
}
