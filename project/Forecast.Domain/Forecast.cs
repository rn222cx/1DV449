﻿using System;
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
        public string WheaterType { get; set; }

        public Forecast(JToken f)
        {
            //City = f["timeseries"][0]["ws"].ToString();
            //WheaterType = (f["timeseries"][0]["tcc"]).ToString();
            City = f["city"]["id"].ToString();
            WheaterType = f["list"][0]["weather"][0]["icon"].ToString();
        }

        //public Forecast(string main)
        //{
        //    City = main;
        //}

    }
}
