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
            NextUpdate = DateTime.Now.AddMinutes(10);
            Wind = wind;
            Degrees = degrees;
            Description = description;
            Rainfall = rainfall == null ? "0" : rainfall; // rainfall data from api doesn't have values when no rain occurred.
        }
    }
}
