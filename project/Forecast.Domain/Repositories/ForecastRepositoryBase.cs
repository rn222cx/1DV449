using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecast.Domain.Repositories
{
    public abstract class ForecastRepositoryBase : IForecastRepository, IDisposable
    {
        protected abstract IQueryable<Weather> QueryWeathers();
        protected abstract IQueryable<Location> QueryLocations();


        public abstract void AddLocation(IEnumerable<Location> location);
        public abstract void AddWeather(IEnumerable<Weather> weather);
        public abstract void DeleteWeather(IEnumerable<Weather> weather);

        //public abstract void AddWeather(Weather weather);

        public abstract void DeleteLocation(int id);

       // public abstract void DeleteWeather(int id);

        public IEnumerable<Location> GetLocation()
        {
            return QueryLocations().ToList();
        }

        public Location GetLocation(int id)
        {
            return QueryLocations().SingleOrDefault(w => w.LocationID == id);
        }
        public abstract IEnumerable<Location> GetCity(string cityName);
        public Location FindCityByName(string cityName)
        {
            return QueryLocations().SingleOrDefault(u => u.City == cityName);
        }

        public IEnumerable<Weather> GetWeather()
        {
            return QueryWeathers().ToList();
        }

        public Weather GetWeather(int id)
        {
            return QueryWeathers().SingleOrDefault(w => w.ForeastID == id);
        }

        public abstract void UpdateLocation(Location location);

        public abstract void UpdateWeather(Weather weather);
        public abstract IEnumerable<Weather> FindWeather(int id);

        public abstract void Save();

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ForecastRepositoryBase() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
