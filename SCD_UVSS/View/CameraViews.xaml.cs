using System.Windows.Controls;
using SCD_UVSS.ViewModel;

namespace SCD_UVSS.View
{
    /// <summary>
    /// Interaction logic for CameraViews.xaml
    /// </summary>
    public partial class CameraViews : UserControl
    {
        public CameraListViewModel CameraListViewModel { get; set; }

        public CameraViews()
        {
            InitializeComponent();

            this.CameraListViewModel = new CameraListViewModel();

            this.DataContext = this.CameraListViewModel;
        }

    }
}
