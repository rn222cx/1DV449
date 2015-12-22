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

        //public override void AddWeather(IEnumerable<Weather> weather)
        //{
        //    foreach (Weather item in weather)
        //    {
        //        _context.Weathers.Add(item);
        //    }
        //}

        public override void AddWeather(Weather weather)
        {
            _context.Weathers.Add(weather);
        }

        public override void DeleteLocation(int id)
        {
            var location = _context.Locations.Find(id);
            _context.Locations.Remove(location);
        }

        //public override void DeleteWeather(IEnumerable<Weather> weather)
        //{
        //    foreach (Weather item in weather)
        //    {
        //        //if (_context.Entry(item).State == EntityState.Detached)
        //        //{
        //        //    _context.Weathers.Attach(item);
        //        //}

        //        _context.Weathers.Remove(item);
        //    }
        //}

        public override void DeleteWeather(int id)
        {
            var weather = _context.Weathers.Find(id);
            _context.Weathers.Remove(weather);
        }

        //public override IEnumerable<Weather> FindWeather(int id)
        //{
        //    var findweather = from Weather in _context.Weathers.ToList()
        //                       where Weather.LocationID == id
        //                       select Weather;

        //    return findweather;
        //}

        //public override IEnumerable<Location> GetCity(string cityName)
        //{
        //    var findCity = from city in _context.Locations.ToList()
        //                   where city.City.ToLower().Contains(cityName.ToLower())
        //                   select city;

        //    return findCity;
        //}

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
            return _context.Locations.AsQueryable();
        }

        protected override IQueryable<Weather> QueryWeathers()
        {
            return _context.Weathers.AsQueryable();
        }

        //public override Location GetLocationById(int id)
        //{
        //    return _context.Locations.Find(id);
        //}

    }
}
