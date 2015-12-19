using Forecast.Domain;
using Forecast.Domain.WebServices;
using Forecast.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forecast.MVC.Controllers
{
    public class ForecastController : Controller
    {
        private Forecast.Domain.IForecastService _service;

        public ForecastController()
            : this(new ForecastService())
        {
            // Empty!
        }

        public ForecastController(Forecast.Domain.IForecastService service)
        {
            _service = service;
        }
        // GET:
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "CityName")] ForecastIndexViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var webservice = new GeoNamesWebService();
                    //model.Locations = webservice.GetLocation(model.CityName); //lat=56.87767&lon=14.80906
                    model.Locations = _service.Getlocation(model.CityName);
                }

                return View(model);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }

            return View(model);
        }
        // GET:
        public ActionResult Weather()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            base.Dispose(disposing);
        }
    }
}