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
        private IRestaurantsManager restaurantsManager{ get; }

        public CitiesController(IRestaurantsManager restaurantsManager)
        {
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