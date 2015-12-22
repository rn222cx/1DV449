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

        public IEnumerable<Weather> GetForecast(Location location)
        {
            string rawJson;

            //string appid = "0b7b7c10d558c6a0cf7ae9b427ad071b";


            var requestUriString = String.Format("http://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&appid=0b7b7c10d558c6a0cf7ae9b427ad071b&units=metric", location.Latitude, location.Longitude);

            var request = (HttpWebRequest)WebRequest.Create(requestUriString);

            using (var response = request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                rawJson = reader.ReadToEnd();
            }

            //var str = new StringBuilder();
            //str.Append("[");
            //str.Append(rawJson);
            //str.Append("]");
            //string result = str.ToString();


            //return JArray.Parse(result).Select(f => new Weather(f)).ToList();

            JObject results = JObject.Parse(rawJson);
            var Forecasts = new List<Weather>();

            foreach (var result in results["list"])
            {
                int locationID = location.LocationID;
                string Period = (string)result["dt_txt"];
                string temp = (string)result["main"]["temp"];
                string symbol = (string)result["weather"][0]["icon"];
                string rainfall = (string)result["rain"]["3h"];
                string wind = (string)result["wind"]["speed"];
                string degrees = (string)result["wind"]["deg"];
                string description = (string)result["weather"][0]["description"];
                Forecasts.Add(new Weather(locationID, Period, symbol, temp, rainfall, wind, degrees, description));
            }

            return Forecasts;

            //public Weather(JToken token)
            //{

            //    //Period = DateTime.ParseExact((token["list"][0]["dt_txt"]).ToString(),
            //    //    "ddd MMM dd HH:mm:ss zz00 yyyy", CultureInfo.InvariantCulture);
            //    Wind = token["list"][0]["wind"]["speed"].ToString();
            //    Degrees = token["list"][0]["wind"]["deg"].ToString();
            //   // Rainfall = token["list"][0]["rain"]["3h"].ToString();
            //    Temp = token["list"][0]["main"]["temp"].ToString();
            //    Description = token["list"][0]["weather"][0]["description"].ToString();
            //    NextUpdate = DateTime.Now.AddMinutes(10);
            //    // City = token.Value<string>("lng");
            //    Symbol = token["list"][0]["weather"][0]["icon"].ToString();


            //    //City = token["city"]["id"].ToString();
            //    //WheaterType = token["list"][0]["weather"][0]["icon"].ToString();
            //    //CreatedAt = DateTime.ParseExact((tweetToken["created_at"]).ToString(),
            //    //    "ddd MMM dd HH:mm:ss zz00 yyyy", CultureInfo.InvariantCulture);
            //}

        }
    }
}
