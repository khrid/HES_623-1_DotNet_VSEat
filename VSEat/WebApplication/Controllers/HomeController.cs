using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private ICitiesManager citiesManager { get; }

        public HomeController(ICitiesManager citiesManager)
        {
            this.citiesManager = citiesManager;
        }

        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("loggedIn") as string)) 
            {
                List<City> cities = citiesManager.GetAllCities();
                return View(cities);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
