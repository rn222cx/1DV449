using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecast.Domain.Repositories
{
    public interface IForecastRepository : IDisposable
    {
        IEnumerable<Weather> FindWeather(int id);
        void AddWeather(Weather weather);
        void DeleteWeather(int id);

        Location GetLocationById(int id);
        IEnumerable<Location> GetCity(string cityName);
        void AddLocation(Location location);
        void DeleteLocation(int id);

        void Save();
    }
}
