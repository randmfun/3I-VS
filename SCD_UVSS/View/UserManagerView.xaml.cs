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
using SCD_UVSS.Dal.UserProvider;
using SCD_UVSS.Model;

namespace SCD_UVSS.View
{
    /// <summary>
    /// Interaction logic for UserManagerView.xaml
    /// </summary>
    public partial class UserManagerView : UserControl
    {
        readonly UserManager _userManager = new UserManager(new BinaryUserProvider());
        readonly List<UserPasswordCtrl> _lstUserPasswordCtrls = new List<UserPasswordCtrl>(); 

        public UserManagerView()
        {
            InitializeComponent();
            this.DefaultLoadUsers();
        }

        private void DefaultLoadUsers()
        {
            var users = this._userManager.GetUsersList();
            foreach (var userInfo in users)
            {
                _lstUserPasswordCtrls.Add(this.CreateUserPasswordCtrl(userInfo));
            }

            this._lstUserPasswordCtrls.ForEach(item => this.spUserList.Children.Add(item));
        }

        private UserPasswordCtrl CreateUserPasswordCtrl(UserInfo userInfo)
        {
            var userPasswordCtrl = new UserPasswordCtrl(userInfo);
            return userPasswordCtrl;
        }

        private void BtnSaveClick_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var lstUserPasswordCtrl in _lstUserPasswordCtrls)
            {
                _userManager.UpdateUserInfo(lstUserPasswordCtrl.UserInfo, lstUserPasswordCtrl.UserNameTxt, lstUserPasswordCtrl.PasswordTxt);
            }
            _userManager.SaveUsers();

            MessageBox.Show("Save Sucessfull !!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
