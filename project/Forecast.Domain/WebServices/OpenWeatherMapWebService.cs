using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace Forecast.Domain.WebServices
{
    public class OpenWeatherMapWebService : IOpenWeatherMapWebService
    {
<<<<<<< HEAD
        public IEnumerable<Weather> GetForecast(Location location)
=======
        public IEnumerable<Forecast> getPlace(string place)
>>>>>>> parent of a0aeb74... ViewModel works for openWeatherMap
        {
            string rawJson;

            //string appid = "0b7b7c10d558c6a0cf7ae9b427ad071b";

<<<<<<< HEAD
            var requestUriString = String.Format("http://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&appid=0b7b7c10d558c6a0cf7ae9b427ad071b&units=metric", location.Latitude, location.Longitude);
=======
            var requestUriString = String.Format("http://api.openweathermap.org/data/2.5/weather?q=Cleveland&APPID=0b7b7c10d558c6a0cf7ae9b427ad071b&mode=json");
>>>>>>> parent of a0aeb74... ViewModel works for openWeatherMap
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

            // Example without stringbulider.

            //JObject results = JObject.Parse(rawJson);
            //var Forecasts = new List<Forecast>();

            //foreach(var result in results["weather"])
            //{
            //    string main = (string)result["main"];
            //    Forecasts.Add(new Forecast(main));
            //}

            //return Forecasts;

        }
    }
}
