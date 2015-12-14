using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace Forecast.Domain
{
    public class Forecast
    {

        public string City { get; set; }
        public string Temp { get; set; }

        public Forecast(JToken f)
        {
            City = (f["coord"]["lat"]).ToString();
            // Temp = (f["coord"]["lat"]).ToString();
        }

        //public Forecast(string main)
        //{
        //    City = main;
        //}

    }
}
