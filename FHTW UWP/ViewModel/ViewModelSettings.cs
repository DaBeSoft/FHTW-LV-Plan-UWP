using System.Threading.Tasks;
using Windows.Storage;
using FHTW_Universal.Logics.Model;
using System;

namespace FHTW_Universal.Logics.ViewModel
{
    public class ViewModelSettings
    {
        public SettingsModel set;

        public ViewModelSettings()
        {
            set = new SettingsModel();
            if (!ApplicationData.Current.LocalSettings.Values.ContainsKey(constantValues.KeyfirstRun))
                ApplicationData.Current.LocalSettings.Values.Add(constantValues.KeyfirstRun, "");
        }

        public void SendMail()
        {
            var uri = new Uri("mailto:office@dabesoft.at?subject=FHTW%20LV%20Plan");
            Windows.System.Launcher.LaunchUriAsync(uri);
        }
    }
}
