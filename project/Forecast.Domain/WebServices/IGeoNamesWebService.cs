using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forecast.Domain.WebServices
{
    public interface IGeoNamesWebService
    {
        IEnumerable<Location> GetLocation(string location);
    }
}
