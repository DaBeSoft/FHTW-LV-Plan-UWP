using FHTW_Universal.Logics;
using FHTW_Universal.Logics.ViewModel;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace FHTW_UWP.Views
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Frames navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ViewModelMainPage vm;
        ApplicationDataContainer localsettings = Windows.Storage.ApplicationData.Current.LocalSettings;


        public MainPage()
        {
            this.InitializeComponent();
            vm = new ViewModelMainPage();

            

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await vm.DownloadLvList();
            vm.SetStackPanel(ref sp);

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Settings));
        }

        private async void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            await vm.DownloadLvList();
            vm.SetStackPanel(ref sp);
        }

        private void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(InfoTerminal));

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var lst =  await ViewModelMainPage.GetLvListFromChache();
            vm.SetStackPanel(ref sp);

            if (!localsettings.Values.ContainsKey(constantValues.KeyfirstRun))
                Frame.Navigate(typeof(Settings));
        }
    }
}
