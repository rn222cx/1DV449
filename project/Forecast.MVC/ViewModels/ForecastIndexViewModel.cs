using Forecast.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forecast.MVC.ViewModels
{
    public class ForecastIndexViewModel
    {
        private bool todayExist;
        private bool tomorrowExist;
        [DisplayName(" ")]
        [Required]
        [MaxLength(50)]
        public string CityName { get; set; }
        public IEnumerable<Weather> Weathers { get; set; }

        public Location location { get; set; }
        public IEnumerable<Location> Locations { get; set; }

        public bool HasCity
        {
            get { return Locations != null && Locations.Any(); }
        }

        public bool HasForecast
        {
            get { return Weathers != null && Weathers.Any(); }
        }

        public string DayOfWeek(DateTime dateTime)
        {

            if (dateTime.DayOfWeek == DateTime.Today.DayOfWeek && todayExist == false)
            {
                todayExist = true;
                return "<h3>Today</h3>";
            }
            if (dateTime.DayOfWeek == DateTime.Now.AddDays(1).DayOfWeek && tomorrowExist == false)
            {
                tomorrowExist = true;
                if(todayExist == false) // If it is late and no today exist
                {
                    return "<h3>Tomorrow</h3>";
                }
                return "</div><div class='weekday'><h3>Tomorrow</h3>";
            }
            if (dateTime.TimeOfDay.ToString() == "00:00:00")
            {
                return "</div><div class='weekday'><h3>" + dateTime.DayOfWeek + "</h3>";
            }
            return null;
        }

    }
}