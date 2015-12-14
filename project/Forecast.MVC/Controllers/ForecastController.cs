using Forecast.Domain.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forecast.MVC.Controllers
{
    public class ForecastController : Controller
    {
        // GET: Forecast
        public ActionResult Index()
        {


            var webservice = new OpenWeatherMapWebService();
            var forecasts = webservice.getPlace("växjö");
            return View(forecasts);
        }
    }
}