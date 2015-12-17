using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecast.Domain
{
    public class Weather
    {
        public string City { get; set; }
        public string WheaterType { get; set; }

        public Weather(JToken f)
        {
            City = f["city"]["id"].ToString();
            WheaterType = f["list"][0]["weather"][0]["icon"].ToString();
        }
    }
}
