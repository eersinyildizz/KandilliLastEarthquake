using DAL.Content.Models;
using DAL.Repository;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HtmlParseSample
{
    public class KandilliDeprem : IKandilliDeprem<Earthquake>
    {
        private const string KandilliUrl = "http://www.koeri.boun.edu.tr/scripts/lst7.asp";
        private List<Earthquake> _earthquake;
        private EarthquakeRepository _earthquakeRepository;
            
        public KandilliDeprem()
        {
            _earthquake = new List<Earthquake>();
            _earthquakeRepository = new EarthquakeRepository();
        }
        public string[] GetEarthquakes()
        {
            Uri url = new Uri(KandilliUrl);

            WebClient client = new WebClient();
            string html = client.DownloadString(url);
            HtmlDocument dokuman = new HtmlDocument();
            dokuman.LoadHtml(html);

            HtmlNodeCollection basliklar = dokuman.DocumentNode.SelectNodes("//pre");

            string result = basliklar[0].InnerHtml;
            int startIndex = result.IndexOf("<pre>") + 5;
            int finishIndex = result.IndexOf("</pre>");

            result = result.Substring(startIndex).Trim();

            result = result.Substring(result.LastIndexOf("------") + 6).Trim();
            string[] strLines = result.Split('\n');

            return strLines;

        }

        public Tuple<List<Earthquake>,int> getDetail(string[] strLines)
        {
            int LastUpdatedIndex = 0;

            for(int i = 0;i<strLines.Length;i++)
            {
               
                string tempItem = strLines[i];
                
                // Date
                int tempIndex = tempItem.IndexOf(" ");
                string Date = tempItem.Substring(0, tempIndex);
                tempItem = tempItem.Substring(tempIndex + 1).Trim();


                //Time
                tempIndex = tempItem.IndexOf("  ");
                string Time = tempItem.Substring(0, tempIndex);
                tempItem = tempItem.Substring(tempIndex + 1).Trim();


                // Latitude
                tempIndex = tempItem.IndexOf(" ");
                string Latitude = tempItem.Substring(0, tempIndex);
                tempItem = tempItem.Substring(tempIndex + 1).Trim();


                // Longitude
                tempIndex = tempItem.IndexOf(" ");
                string Longitude = tempItem.Substring(0, tempIndex);
                tempItem = tempItem.Substring(tempIndex + 1).Trim();


                // Depth 
                tempIndex = tempItem.IndexOf(" ");
                string Depth = tempItem.Substring(0, tempIndex);
                tempItem = tempItem.Substring(tempIndex + 1).Trim();

                // Magnitude
                tempIndex = tempItem.IndexOf(" ");
                tempItem = tempItem.Substring(tempIndex + 1).Trim();
                tempIndex = tempItem.IndexOf(" ");
                string Magnitude = tempItem.Substring(0, tempIndex);
                tempItem = tempItem.Substring(tempIndex + 1).Trim();

                // Address
                tempIndex = tempItem.IndexOf(" ");
                tempItem = tempItem.Substring(tempIndex + 1).Trim();
                tempIndex = tempItem.IndexOf(" ");
                string Address = tempItem.Substring(0, tempIndex);
                tempItem = tempItem.Substring(tempIndex + 1).Trim();

                Earthquake model = new Earthquake
                {
                    Date = DateTime.Parse(Date),
                    Time = DateTime.Parse(Time),
                    Latitude = Convert.ToDouble(Latitude),
                    Longitude = Convert.ToDouble(Longitude),
                    Depth = Convert.ToDouble(Depth),
                    Magnitude = Convert.ToDouble(Magnitude),
                    Address = Address
                };
                  
                /// Just todays earthquake selected
                if (model.Date != DateTime.UtcNow.Date)
                {                  
                    break;
                }

                /// Last added item and new coming items are comparing. If are there equals then we exactly know that where we are
                if (model.Time.TimeOfDay.Equals(GetLastInsertedItem().TimeOfDay))
                {
                    LastUpdatedIndex = i;
                }

               // Console.WriteLine("Index is : "+i);
                _earthquake.Add(model);
               

                //Console.WriteLine("Start Date : " + Date + " Time : " + Time + " Latitude : " + Latitude + " Longitude : " + Longitude + " Depth : " + Depth + " Magnitude : " + Magnitude + " Address :" + Address);

            }
            

            return Tuple.Create(_earthquake, LastUpdatedIndex);
        }

        public void Add(List<Earthquake> items, int startIndex)
        {
            if (startIndex == 0)
            {                                
                // If db is null than it means that application is started firstly
                if (GetLastInsertedItem() == null)
                {
                    for (int i = items.Count - 1; i >= 0; i--)
                    {
                        _earthquakeRepository.Add(items[i]);
                    }
                    return;
                }
                Console.WriteLine("There is not new earthquake :) ");
            }
            else 
            {
                for (int i = startIndex - 1; i >= 0; i--)
                {
                    _earthquakeRepository.Add(items[i]);
                }
            }            
        }

        public DateTime GetLastInsertedItem()
        {
            var result = _earthquakeRepository.Get(null, 1);
            if(result[0]!=null)
                return result[0].Time;
            return DateTime.UtcNow;
        }

        
    }
}
