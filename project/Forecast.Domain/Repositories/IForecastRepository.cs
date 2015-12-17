using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecast.Domain.Repositories
{
    public interface IForecastRepository : IDisposable
    {
        IEnumerable<Weather> GetWeather();
        Weather GetWeather(int id);
        void AddWeather(Weather weather);
        void UpdateWeather(Weather weather);
        void DeleteWeather(int id);

        IEnumerable<Location> GetLocation();
        Location GetLocation(int id);
        void AddLocation(Location location);
        void UpdateLocation(Location location);
        void DeleteLocation(int id);

        void Save();
    }
}
