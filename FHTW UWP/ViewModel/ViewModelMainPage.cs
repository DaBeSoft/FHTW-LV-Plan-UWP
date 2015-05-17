using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using FHTW_Universal.Logics.Model;
using FHTW_Universal.Logics.View;

namespace FHTW_Universal.Logics.ViewModel
{
    public class ViewModelMainPage
    {
        public ViewModelMainPage()
        {
            _settingsModel = new SettingsModel();
        }

        readonly SettingsModel _settingsModel;
        public ObservableCollection<StundenplanEintrag> LvList;


        public bool CacheExists() { return false; }

        public bool LoggedIn() { return true; }



        public static async Task<ObservableCollection<StundenplanEintrag>> GetLvListFromChache()
        {
            try
            {
                StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                StorageFile sampleFile = await localFolder.GetFileAsync("cache.txt");
                string timestamp = await FileIO.ReadTextAsync(sampleFile);
                return new LVHandler().TextToEinträge(timestamp);
            }
            catch
            {
                return new ObservableCollection<StundenplanEintrag>();
            }
        }

        public async Task DownloadLvList()
        {
            try
            {
                LvList = await new LVHandler().GetLVPlanAsync(_settingsModel.Username, _settingsModel.Password);
            }
            catch
            {
                var a = new Windows.UI.Popups.MessageDialog("Konnte keine Daten abrufen. Überprüfen Sie ihre Logindaten und Ihre Internetverbindung!");
                a.ShowAsync();
            }
        }

        public void SetStackPanel(ref StackPanel sp)
        {
            sp.Children.Clear();
            var maxDays = new TimeSpan(_settingsModel.DaysToShow, 0, 0, 0);

            var first = true;


            if (LvList == null || LvList.Count == 0)
            {

                var tb = new TextBlock
                {
                    Text = "Keine Daten gecached!\r\n\r\nKlicken Sie auf aktualisieren um neue\r\nDaten abzurufen.",
                    FontSize = 20
                };
                sp.Children.Add(tb);
                return;
            }


            var current = new DayControl();
            var lastDayControl = DateTime.Now;
            foreach (var se in LvList)
            {
                if (DateTime.Now.Date > se.Start.Date)
                    continue;

                if (lastDayControl.Date < se.Start.Date)
                {

                    if (first)
                    {
                        first = false;
                        current.open();
                    }

                    if ((se.Start.Date - DateTime.Now.Date) > maxDays)
                        break;

                    if (current.ChildrenCount > 0)
                    {
                        sp.Children.Add(current);
                    }

                    lastDayControl = se.Start;
                    current = new DayControl();
                }

                current.DataContext = se;
                var lvc = new LVControl { DataContext = se };
                current.AddChildren(lvc);
            }
            if (current.ChildrenCount > 0 && !sp.Children.Contains(current))
                sp.Children.Add(current);

            if(sp.Children.Count == 0)
            {
                var tb = new TextBlock
                {
                    Text = string.Format("Keine Lehrveranstaltungen\r\nin den nächsten {0} Tagen!", maxDays.Days),//\r\n\r\nKlicken Sie auf aktualisieren um neue\r\nDaten abzurufen.",
                    FontSize = 20
                };
                sp.Children.Add(tb);
            }
        }
    }
}
