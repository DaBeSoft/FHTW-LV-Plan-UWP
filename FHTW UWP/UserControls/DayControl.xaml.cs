using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace FHTW_Universal.Logics.View
{
    public sealed partial class DayControl : UserControl
    {

        bool opened = false;
        int maxHeight = 80;
        public DayControl()
        {
            this.InitializeComponent();
            //stackPanel1.Height = 0;
            //maxHeight += 80;
            this.Height = maxHeight;
            
        }

        public int ChildrenCount { get { return stackPanel1.Children.Count; } }

        public void AddChildren(LVControl uie)
        {
            //uie.DataContext = this.DataContext;
            maxHeight += 80;
            stackPanel1.Children.Add(uie);
        }

        public void open()
        {
            opened = true;
            this.Height = maxHeight;
            ButtonOpenClose.Content = "^";
        }

        void close()
        {
            opened = false;
            this.Height = 80;
            ButtonOpenClose.Content = "v";

        }

        private void ButtonOpenClose_Tapped(object sender, TappedRoutedEventArgs e)
        {
            doOpenClose();
        }

        private void doOpenClose()
        {
            if (!opened)
            {
                open();
            }
            else
            {
                close();
            }
        }
    }
}
