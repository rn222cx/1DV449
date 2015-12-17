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
        // GET:
        public  ActionResult Index()
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
                    var webservice = new GeoNamesWebService();
                    model.Locations = webservice.GetLocation(model.CityName); //lat=56.87767&lon=14.80906
                }
                
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

    }
}