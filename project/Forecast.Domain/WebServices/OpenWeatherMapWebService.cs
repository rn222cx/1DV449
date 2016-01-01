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
using System.Web.Configuration;
using System.Configuration;
using System.Web.Script.Serialization;

namespace Forecast.Domain.WebServices
{
    public class OpenWeatherMapWebService : IOpenWeatherMapWebService
    {

        public IEnumerable<Weather> GetForecast(Location location)
        {
            string rawJson;

            string appid = WebConfigurationManager.AppSettings["owmKey"];

            var requestUriString = String.Format("http://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&appid={2}&units=metric", location.Latitude, location.Longitude, appid);

            var request = (HttpWebRequest)WebRequest.Create(requestUriString);

            using (var response = request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                rawJson = reader.ReadToEnd();
            }

            JObject results = JObject.Parse(rawJson);
            var Forecasts = new List<Weather>();

            foreach (var result in results["list"])
            {
                int locationID = location.LocationID;
                string Period = (string)result["dt_txt"];
                string temp = (string)result["main"]["temp"];
                string symbol = (string)result["weather"][0]["icon"];
                string rainfall;

                // Rain and snow is not included in every places so this is my hack for it.

                try
                {
                    rainfall = (string)result["rain"]["3h"];
                }
                catch
                {
                    rainfall = "0";
                }

                string wind = (string)result["wind"]["speed"];
                string degrees = (string)result["wind"]["deg"];
                string description = (string)result["weather"][0]["description"];
                Forecasts.Add(new Weather(locationID, Period, symbol, temp, rainfall, wind, degrees, description));
            }

            return Forecasts;

        }

    }
}
