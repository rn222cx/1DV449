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
        IEnumerable<Weather> GetWeather();
        Weather GetWeather(int id);
        //void AddWeather(Weather weather);
        void AddWeather(IEnumerable<Weather> weather);

        void UpdateWeather(Weather weather);
        void DeleteWeather(int id);

        IEnumerable<Location> GetLocation();
        Location GetLocation(int id);
        IEnumerable<Location> GetCity(string cityName);
        void AddLocation(IEnumerable<Location> location);
        //void AddLocation(Location location);
        void UpdateLocation(Location location);
        void DeleteLocation(int id);

        void Save();
    }
}
