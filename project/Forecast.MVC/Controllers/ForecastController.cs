using Forecast.Domain;
using Forecast.Domain.WebServices;
using Forecast.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forecast.MVC.Controllers
{
    public class ForecastController : Controller
    {
        private IForecastService _service;

        public ForecastController()
            : this(new ForecastService())
        {
            // Empty!
        }

        public ForecastController(IForecastService service)
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
                    model.Locations = _service.Getlocation(model.CityName);
                    if (!model.Locations.Any())
                    {
                        TempData["error"] = "Could not found location named " + model.CityName;
                    }
                }

                return View(model);

            }
            catch (DataException)
            {
                TempData["error"] = "Sorry, lost connection";
            }

            return View(model);
        }
        // GET Weather:
        public ActionResult Weather(int id, ForecastIndexViewModel model)
        {
            try
            {
                model.location = _service.GetLocationById(id);
                model.Weathers = _service.RefreshWeather(model.location);
            }
            catch (DataException)
            {
                TempData["error"] = "Sorry, could not get forecast";
            }

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            base.Dispose(disposing);
        }
    }
}