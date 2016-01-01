using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecast.Domain
{
    public partial class Location
    {
        public Location(JToken token)
            : this()
        {
            // Sometimes name on city(often airports) includes long names, if longer than 49 it will strip down and add dots...
            string cityNameLength = token.Value<string>("name");

            City = (cityNameLength.Length > 49) ? cityNameLength.Substring(0, 45) + "..." : cityNameLength;
            Country = token.Value<string>("countryName");
            County = token.Value<string>("adminName1");
            Latitude = token.Value<double>("lat");
            Longitude = token.Value<double>("lng");
        }
    }
}
