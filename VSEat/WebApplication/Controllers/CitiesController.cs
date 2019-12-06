using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebApplication.Controllers
{
    public class CitiesController : Controller
    {
        private ICitiesManager citiesManager { get; }
        private IRestaurantsManager restaurantsManager{ get; }

        public CitiesController(ICitiesManager citiesManager, IRestaurantsManager restaurantsManager)
        {
            this.citiesManager = citiesManager;
            this.restaurantsManager = restaurantsManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List(int id)
        {
            List<DTO.Restaurant> cities = restaurantsManager.GetRestaurantByCity(id);
            return View(cities);
        }
    }
}