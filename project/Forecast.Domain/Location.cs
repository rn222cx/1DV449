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
            Country = token.Value<string>("countryName");
            County = token.Value<string>("adminName1");
            City = token.Value<string>("name");
            Latitude = token.Value<double>("lat");
            Longitude = token.Value<double>("lng");
        }
    }
}
