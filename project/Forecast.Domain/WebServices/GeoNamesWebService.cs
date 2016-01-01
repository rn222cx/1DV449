using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Forecast.Domain.WebServices
{
    public class GeoNamesWebService : IGeoNamesWebService
    {
        public IEnumerable<Location> GetLocation(string location)
        {
            string rawJson;
            string appid = WebConfigurationManager.AppSettings["geoKey"];

            var requestUriString = String.Format("http://api.geonames.org/searchJSON?name={0}&maxRows=50&username={1}", location, appid);
            var request = (HttpWebRequest)WebRequest.Create(requestUriString);

            using (var response = request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                rawJson = reader.ReadToEnd();
            }

            var startContent = rawJson.IndexOf("[");
            var lengthContent = rawJson.Length;
            var content = rawJson.Substring(startContent, lengthContent - startContent - 1);

            return JArray.Parse(content).Select(f => new Location(f)).ToList();
        }
    }
}
