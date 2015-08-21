using System.Windows;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "mylog4net.config", Watch = true)]

namespace SCD_UVSS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
    }
}
