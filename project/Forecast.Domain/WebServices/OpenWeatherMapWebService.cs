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
    public class OpenWeatherMapWebService
    {
        public IEnumerable<Weather> getPlace(string place)
        {
            string rawJson;

            //var path = HttpContext.Current.Server.MapPath("~/App_Data/owm.json");
            //using (StreamReader reader = new StreamReader(path))
            //{
            //    rawJson = reader.ReadToEnd();
            //}

            //string appid = "0b7b7c10d558c6a0cf7ae9b427ad071b";

            var requestUriString = String.Format("http://api.openweathermap.org/data/2.5/forecast?{0}&appid=0b7b7c10d558c6a0cf7ae9b427ad071b", place);
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

            return JArray.Parse(result).Select(f => new Weather(f)).ToList();

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
