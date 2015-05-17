using FHTW_Universal.Logics.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace FHTW_UWP.Views
{
    public sealed partial class Settings : Page
    {
        readonly ViewModelSettings _vm;

        public Settings()
        {
            InitializeComponent();
            _vm = new ViewModelSettings();
            DataContext = _vm.set;

            if (App.isBackButtonPressedAvailable)
            {
                Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += Settings_BackRequested;
                BackButton.Visibility = Visibility.Collapsed;
            }
        }

        private void Settings_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            e.Handled = true;
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested -= Settings_BackRequested;
            Frame.Navigate(typeof(MainPage));
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void ContactButton_Click(object sender, RoutedEventArgs e)
        {
            _vm.SendMail();
        }
    }
}
