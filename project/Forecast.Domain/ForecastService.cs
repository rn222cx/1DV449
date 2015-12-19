using Forecast.Domain.Repositories;
using Forecast.Domain.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if (city.Count() == 0)
            {
                city = _geoWebservice.GetLocation(cityName);

                _repository.AddLocation(city);
                _repository.Save();
            }

            return city;
        }

        public override IEnumerable<Weather> RefreshWeather(Location location)
        {
            var weather = _repository.FindWeather(location.LocationID);

            if (weather.Count() == 0)
            {
                weather = _owmWebservice.GetForecast(location);
                _repository.AddWeather(weather);
                _repository.Save();
            }
            else
            {
                foreach (Weather item in weather)
                {
                    if (item.NextUpdate < DateTime.Now)
                    {
                        _repository.DeleteWeather(weather);
                        _repository.Save();

                        weather = _owmWebservice.GetForecast(location);

                        _repository.AddWeather(weather);
                        _repository.Save();
                        break;
                    }
                }
            }

            return weather;
        }

        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
            base.Dispose(disposing);
        }
    }
}
