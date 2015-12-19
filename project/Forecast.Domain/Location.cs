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
        //public Location()
        //{
        //    // Empty!
        //}
        //public string Country { get; set; }
        //public string County { get; set; }
        //public string City { get; set; }
        //public string Latitude { get; set; }
        //public string Longitude { get; set; }

        public Location(JToken token)
            : this()
        {
            Country = token["countryName"].ToString();
            County = token["adminName1"].ToString();
            City = token["name"].ToString();
            Latitude = token.Value<double>("lat");
            Longitude = token.Value<double>("lng");
            //Longitude = token["lng"].ToString();
        }
    }
}
