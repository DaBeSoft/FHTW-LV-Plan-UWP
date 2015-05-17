using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Windows.Storage;
using FHTW_Universal.Logics.Model;

namespace FHTW_Universal.Logics
{
    public class LVHandler
    {

        public async Task<ObservableCollection<StundenplanEintrag>> GetLVPlanAsync(string username, string password)
        {
            if (username == constantValues.testUsernameValue)
                return CreateTestEntrys();

            string downloadstring = string.Format(constantValues.LVDownloadURI, username);

            WebRequest wr = WebRequest.CreateHttp(new Uri(downloadstring));
            wr.Credentials = new NetworkCredential(username, password);

            var response = await wr.GetResponseAsync();

            var stream = response.GetResponseStream();

            StreamReader sr = new StreamReader(stream);
            string responsetxt = sr.ReadToEnd();

            int lol = responsetxt.Length;
            ObservableCollection<StundenplanEintrag> Einträge = TextToEinträge(responsetxt);

            Windows.Storage.StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await localFolder.CreateFileAsync("cache.txt", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(sampleFile, responsetxt);
            
            return Einträge;
        }

        private ObservableCollection<StundenplanEintrag> CreateTestEntrys()
        {
            string allText = "line1\r\n";
            DateTime start = DateTime.Now.AddHours(2);
            DateTime end = DateTime.Now.AddHours(4);
            for (int i = 0; i < 10; i++)
            {
                Random rnd = new Random(i);
                string fach = "TestLV" + rnd.Next(100).ToString();
                string raum = "Raum" + rnd.Next(100).ToString();

                string s = "\"{0}\",\"StundenplanTW\",\"{1}\",\"Stundenplan\r\n";
                s += "CGE\r\n";
                s += "HesinaGe\r\n";
                s += "BIF-4CGE2\r\n";
                s += "EDV_F1.03\",\"Stundenplan\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",,,,,";

                s = string.Format(s, fach, raum, start.ToString("dd.MM.yyyy"), start.ToString("hh:mm:ss"), end.ToString("dd.MM.yyyy"), end.ToString("hh:mm:ss"));

                //Einträge.Add(new StundenplanEintrag(s));
                allText += s;
                start = start.AddDays(1);
                end = end.AddDays(1);
                allText += s + "\r\n";
            }
            return TextToEinträge(allText);
        }



        public ObservableCollection<StundenplanEintrag> TextToEinträge(string allText)
        {
            ObservableCollection<StundenplanEintrag> Einträge = new ObservableCollection<StundenplanEintrag>();
            StringReader sr = new StringReader(allText);
            sr.ReadLine();
            while (sr.Peek() != -1)
            {
                string s = sr.ReadLine();
                while (s.CharCount('"') % 2 == 1)
                {
                    s += "\r\n" + sr.ReadLine();
                }
                StundenplanEintrag se = new StundenplanEintrag(s);
                Einträge.Add(se);
            }
            return Einträge;
        }
    }
}
