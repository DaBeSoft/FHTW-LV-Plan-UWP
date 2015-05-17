using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace FHTW_UWP.Views
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class InfoTerminal : Page
    {
        public InfoTerminal()
        {
            this.InitializeComponent();
            
        }

        /// <summary>
        /// Wird aufgerufen, wenn diese Seite in einem Frame angezeigt werden soll.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.isBackButtonPressedAvailable)
            {
                Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += Settings_BackRequested;
                appBar.Visibility = Visibility.Collapsed;
            }
            webView1.Navigate(new Uri("https://cis.technikum-wien.at/cis/infoterminal/"));
            webView1.NavigationCompleted += (view, args) => invistext.Focus(FocusState.Programmatic);
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
    }
}
