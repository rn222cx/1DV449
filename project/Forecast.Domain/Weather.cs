using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecast.Domain
{
    public partial class Weather
    {
        public Weather()
        {
            // Empty!        
        }

        public Weather(int locationID, string period, string symbol, string temp, string rainfall, string wind, string degrees, string description)
        {
            LocationID = locationID;
            Period = DateTime.Parse(period);
            Temp = temp;
            Symbol = symbol;
            NextUpdate = DateTime.Now.AddMinutes(5);
            Wind = wind;
            Degrees = degrees;
            Description = description;
            Rainfall = rainfall == null ? "0" : rainfall;
        }

        //public Weather(JToken token)
        //{
        //    LocationID = 5;
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
