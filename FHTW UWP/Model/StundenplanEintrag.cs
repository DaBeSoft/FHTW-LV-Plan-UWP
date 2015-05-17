using System;
using System.ComponentModel;

namespace FHTW_Universal.Logics.Model
{
    public class StundenplanEintrag : INotifyPropertyChanged
    {
        public StundenplanEintrag(string line)
        {
            string[] tmp = line.Split(new char[] { ',' }, StringSplitOptions.None);
            LVName = tmp[0];
            Room = tmp[2];
            //teacher = "";
            string s = tmp[5].Trim('"') + " " + tmp[6].Trim('"');
            Start = DateTime.Parse(s);
            s = tmp[7].Trim('"') + " " + tmp[8].Trim('"');
            End = DateTime.Parse(s);
        }

        DateTime _start;
        public DateTime Start
        {
            get
            {
                return _start;
            }
            set
            {
                if (_start != value)
                {
                    _start = value;
                    RaisePropertyChanged("Start");
                }
            }
        }

        DateTime _end;
        public DateTime End
        {
            get
            {
                return _end;
            }
            set
            {
                if (_end != value)
                {
                    _end = value;
                    RaisePropertyChanged("End");
                }
            }
        }

        string _lvName;
        public string LVName
        {
            get
            {
                return _lvName.Trim('"');
            }
            set
            {
                if (_lvName != value)
                {
                    _lvName = value;
                    RaisePropertyChanged("LVName");
                }
            }
        }

        string _room;
        public string Room
        {
            get
            {
                return _room.Trim('"');
            }
            set
            {
                if (_room != value)
                {
                    _room = value;
                    RaisePropertyChanged("Room");
                }
            }
        }

        public string DayName
        {
            get
            {
                string dayName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)Start.DayOfWeek];
                return string.Format("{0:00}.{1:00}.{2} - {3}", Start.Day, Start.Month, Start.ToString("yy"), dayName);
            }
        }

        public string FromTo
        {
            get
            {
                return string.Format("{0} - {1}", Start.ToString("HH:mm"), End.ToString("HH:mm"));
            }
        }




        void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;


    }
}
