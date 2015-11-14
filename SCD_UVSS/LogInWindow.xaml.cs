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
using System.Windows.Shapes;
using SCD_UVSS.Dal;
using SCD_UVSS.Dal.UserProvider;

namespace SCD_UVSS
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();
        }

        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            Validate();
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Validate()
        {
            var userName = this.txtUserName.Text;
            var password = this.txtPassword.Password;

            string errorMsg;
            var validUser = UserManager.Instance.IsUserValid(userName, password, out errorMsg);

            if (!validUser)
            {
                MessageBox.Show(errorMsg, "Login Error:", MessageBoxButton.OK, MessageBoxImage.Error);
                this.DialogResult = false;
            }
            else
            {
                UserManager.Instance.SetLoggedInUser(userName);
                this.DialogResult = true;  
            }
        }

        private void LogInWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.OK_OnClick(null, null);
            }
        }

    }
}
