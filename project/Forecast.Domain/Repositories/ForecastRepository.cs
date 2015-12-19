using Forecast.Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecast.Domain.Repositories
{
    class ForecastRepository : ForecastRepositoryBase
    {
        private readonly WP14_rn222cx_WeatherEntities _context = new WP14_rn222cx_WeatherEntities(); 

        public override void AddLocation(IEnumerable<Location> location)
        {
            foreach (Location item in location)
            {
                _context.Locations.Add(item); 
            }
        }


        public override void AddWeather(Weather weather)
        {
            _context.Weathers.Add(weather);
        }

        public override void DeleteLocation(int id)
        {
            var location = _context.Locations.Find(id);
            _context.Locations.Remove(location);
        }

        public override void DeleteWeather(int id)
        {
            var weather = _context.Weathers.Find(id);
            _context.Weathers.Remove(weather);
        }

        public override IEnumerable<Location> GetCity(string cityName)
        {
            var findCity = from city in _context.Locations.ToList()
                           where city.City.ToLower().Contains(cityName.ToLower())
                           select city;

            return findCity;
        }

        public override void Save()
        {
            _context.SaveChanges();
        }

        public override void UpdateLocation(Location location)
        {
            _context.Entry(location).State = EntityState.Modified;
        }

        public override void UpdateWeather(Weather weather)
        {
            _context.Entry(weather).State = EntityState.Modified;
        }

        protected override IQueryable<Location> QueryLocations()
        {
            return _context.Locations;
        }

        protected override IQueryable<Weather> QueryWeathers()
        {
            return _context.Weathers;
        }
    }
}
