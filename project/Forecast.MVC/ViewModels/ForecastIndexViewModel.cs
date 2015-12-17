using Forecast.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forecast.MVC.ViewModels
{
    public class ForecastIndexViewModel
    {
        public string CityName { get; set; }
        public IEnumerable<Weather> Weathers { get; set; }

        public Location location { get; set; }
        public IEnumerable<Location> Locations { get; set; }

        public bool HasCity
        {
            get { return Locations != null && Locations.Any(); }
        }

        public string Name
        {
            get
            {
                return HasCity ? Weathers.First().City : "[Unknown]";
            }
        }


    }
}