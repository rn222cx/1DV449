using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Forecast.Domain.WebServices
{
    public class SMHIService
    {
        public IEnumerable<Forecast> getPlace(string place)
        {
            string rawJson;

            var requestUriString = String.Format("http://opendata-download-metfcst.smhi.se/api/category/pmp1.5g/version/1/geopoint/lat/58.59/lon/16.18/data.json");
            var request = (HttpWebRequest)WebRequest.Create(requestUriString);

            using (var response = request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                rawJson = reader.ReadToEnd();
            }

            var str = new StringBuilder();
            str.Append("[");
            str.Append(rawJson);
            str.Append("]");
            string result = str.ToString();

            return JArray.Parse(result).Select(f => new Forecast(f)).ToList();

            //wheaterType:
            //{
            //    0: 'Ingen nederbörd',
            //    1: 'Snö',
            //    2: 'Snöblandat regn',
            //    3: 'Regn',
            //    4: 'Duggregn',
            //    5: 'Snö',
            //    6: 'Snö'
            //},

            //cloudType:
            //        {
            //            0: 'Klart',
            //    1: 'Klart',
            //    2: 'Halvklart',
            //    3: 'Halvklart',
            //    4: 'Halvklart till mulet',
            //    5: 'Halvklart till mulet',
            //    6: 'Mulet',
            //    7: 'Mulet',
            //    8: 'Mulet'
            //},
        }
    }
}
