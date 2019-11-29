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
        private IConfiguration Configuration { get; }

        public CitiesController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List(int id)
        {
            RestaurantsManager restaurantManager = new RestaurantsManager(Configuration);
            List<DTO.Restaurant> cities = restaurantManager.GetRestaurantByCity(id);
            return View(cities);
        }
    }
}