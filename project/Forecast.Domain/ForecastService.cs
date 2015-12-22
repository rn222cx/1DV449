using Forecast.Domain.Repositories;
using Forecast.Domain.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Forecast.Domain
{
    public class ForecastService : ForecastServiceBase
    {
        private readonly IForecastRepository _repository;
        private readonly IOpenWeatherMapWebService _owmWebservice;
        private readonly IGeoNamesWebService _geoWebservice;

        public ForecastService()
            : this(new ForecastRepository(), new GeoNamesWebService(), new OpenWeatherMapWebService())
        {
        }

        public ForecastService(IForecastRepository repository, IGeoNamesWebService geoWebservice, IOpenWeatherMapWebService owmWebservice )
        {
            _repository = repository;
            _owmWebservice = owmWebservice;
            _geoWebservice = geoWebservice;
        }

        public override IEnumerable<Location> Getlocation(string cityName)
        {
            var city = _repository.GetCity(cityName);

            if (!city.Any())
            {
                city = _geoWebservice.GetLocation(cityName);
                foreach (var loc in city)
                {
                    _repository.AddLocation(loc);
                }

                _repository.Save();
            }

            return city;
        }

        public override IEnumerable<Weather> RefreshWeather(Location location)
        {
            var weather = _repository.FindWeather(location.LocationID);
            if (!weather.Any() || weather.Any(x => x.NextUpdate < DateTime.Now))
            {
                foreach (var item in weather)
                { 
                    _repository.DeleteWeather(item.WeatherID);                                
                }

                weather = _owmWebservice.GetForecast(location);

                foreach (var item in weather)
                {
                    _repository.AddWeather(item);                                    
                }


                _repository.Save();
            }

            return weather;
        }

        public override Location GetLocationById(int id)
        {
            return _repository.GetLocationById(id);
        }

        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
            base.Dispose(disposing);
        }
    }
}
