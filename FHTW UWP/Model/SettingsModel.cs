using System.ComponentModel;
using Windows.Storage;

namespace FHTW_Universal.Logics.Model
{
    public class SettingsModel : INotifyPropertyChanged
    {
        readonly ApplicationDataContainer _roamingSettings = ApplicationData.Current.RoamingSettings;

        public string Username
        {
            get
            {
                if (!_roamingSettings.Values.ContainsKey(constantValues.Keyusername))
                    _roamingSettings.Values.Add(constantValues.Keyusername, "");
                return (string)_roamingSettings.Values[constantValues.Keyusername];
            }
            set
            {
                if ((string)_roamingSettings.Values[constantValues.Keyusername] != value)
                {
                    _roamingSettings.Values[constantValues.Keyusername] = value;
                    RaisePropertyChanged("Username");
                }
            }
        }

        public string Password
        {
            get
            {
                if (!_roamingSettings.Values.ContainsKey(constantValues.Keypassword))
                    _roamingSettings.Values.Add(constantValues.Keypassword, "");
                return (string)_roamingSettings.Values[constantValues.Keypassword];
            }
            set
            {
                if ((string)_roamingSettings.Values[constantValues.Keypassword] != value)
                {
                    _roamingSettings.Values[constantValues.Keypassword] = value;
                    RaisePropertyChanged("Password");
                }
            }
        }

        public bool ShowEmptyDays
        {
            get
            {
                if (!_roamingSettings.Values.ContainsKey(constantValues.KeyshowEmpty))
                    _roamingSettings.Values.Add(constantValues.KeyshowEmpty, false);
                return (bool)_roamingSettings.Values[constantValues.KeyshowEmpty];
            }
            set
            {
                if ((bool)_roamingSettings.Values[constantValues.KeyshowEmpty] != value)
                {
                    _roamingSettings.Values[constantValues.KeyshowEmpty] = value;
                    RaisePropertyChanged("ShowEmptyDays");
                }
            }
        }

        public int DaysToShow
        {
            get
            {
                if (!_roamingSettings.Values.ContainsKey(constantValues.KeyDaysToShow))
                    _roamingSettings.Values.Add(constantValues.KeyDaysToShow, 10);
                return (int)_roamingSettings.Values[constantValues.KeyDaysToShow];
            }
            set
            {
                if ((int)_roamingSettings.Values[constantValues.KeyDaysToShow] != value)
                {
                    _roamingSettings.Values[constantValues.KeyDaysToShow] = value;
                    RaisePropertyChanged("DaysToShow");
                    RaisePropertyChanged("DaysToShowText");
                }
            }
        }

        public string DaysToShowText
        {
            get
            {
                return string.Format("Zeige {0} Tage", DaysToShow);
            }
        }

        void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
