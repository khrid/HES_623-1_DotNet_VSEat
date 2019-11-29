using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebApplication.Controllers
{
    public class DishesController : Controller
    {
        private IConfiguration Configuration { get; }

        public DishesController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List(int id)
        {
            DishesManager dishesManager = new DishesManager(Configuration);
            RestaurantsManager restaurantsManager = new RestaurantsManager(Configuration);
            List<DTO.Dish> dishes = dishesManager.GetAllDishesForRestaurant(id);
            ViewBag.restaurantName = restaurantsManager.GetRestaurantById(id).merchant_name;
            return View(dishes);
        }
    }
}