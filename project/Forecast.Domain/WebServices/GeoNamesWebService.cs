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
    public class GeoNamesWebService : IGeoNamesWebService
    {
        public IEnumerable<Location> GetLocation(string location)
        {
            string rawJson;

            var requestUriString = String.Format("http://api.geonames.org/searchJSON?name={0}&maxRows=50&username=rn222cx", location);
            var request = (HttpWebRequest)WebRequest.Create(requestUriString);

            using (var response = request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                rawJson = reader.ReadToEnd();
            }

            var lengthToStart = rawJson.IndexOf("[");
            var lengthRawJson = rawJson.Length;
            var contentRawJson = rawJson.Substring(lengthToStart, lengthRawJson - lengthToStart - 1);

            return JArray.Parse(contentRawJson).Select(f => new Location(f)).ToList();
        }
    }
}
