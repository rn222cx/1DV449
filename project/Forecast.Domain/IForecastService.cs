using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecast.Domain
{
    public interface IForecastService : IDisposable
    {
        IEnumerable<Location> Getlocation(string cityName);
        IEnumerable<Weather> RefreshWeather(Location location);

        Location GetLocationById(int id);
        //Location Getlocation(string cityName);
        //void RefreshWeather(Location location);
    }
}
