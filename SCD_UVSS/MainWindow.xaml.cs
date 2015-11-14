using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using log4net;
using SCD_UVSS.Controller;
using SCD_UVSS.ViewModel;

namespace SCD_UVSS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(MainWindow));
        
        public static bool _windowStateFullScreen = true;

        private MainTabViewModel _mainTabViewModel;

        public MainWindow()
        {
            try
            {
                logger.Info("Application Starting!!");

                if (!this.GrantAccess())
                {
                    logger.Error("Log in Failed!!");
                    this.Close();
                    return;
                }

                logger.Info("Logged In!!");

                InitializeComponent();
                this.InitializeApplication();

                logger.Info("Application Started!!");
                
            }
            catch (Exception ex)
            {
                logger.Error("Application Crashed.", ex);
                logger.Fatal(ex.Message, ex);
                MessageBox.Show("Application Crashed!! Check Log file for more details!!");
                this.Close();
            }
        }
        
        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (_windowStateFullScreen)
                {
                    this.AppWindow.WindowState = WindowState.Normal;
                    this.AppWindow.WindowStyle = WindowStyle.ThreeDBorderWindow;
                }
                else
                {
                    this.AppWindow.WindowState = WindowState.Maximized;
                    this.AppWindow.WindowStyle = WindowStyle.SingleBorderWindow;
                }
                _windowStateFullScreen = !_windowStateFullScreen;
            }
        }

        private bool GrantAccess()
        {
            logger.Info("Show Log In Screen!!");

            var loginWindow = new LogInWindow();
            var result = loginWindow.ShowDialog();

            if (result.HasValue)
                return result.Value;

            return false;
        }

        private void InitializeApplication()
        {
            this._mainTabViewModel = new MainTabViewModel();
            this.mainTabCtrl.DataContext = this._mainTabViewModel;
        }
    }
    
}
