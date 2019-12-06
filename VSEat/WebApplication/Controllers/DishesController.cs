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
        private IDishesManager dishesManager { get; }
        private IRestaurantsManager restaurantsManager { get; }

        public DishesController(IDishesManager dishesManager, IRestaurantsManager restaurantsManager)
        {
            this.dishesManager = dishesManager;
            this.restaurantsManager = restaurantsManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List(int id)
        {
            List<DTO.Dish> dishes = dishesManager.GetAllDishesForRestaurant(id);
            ViewBag.restaurantName = restaurantsManager.GetRestaurantById(id).merchant_name;
            ViewBag.restaurantId = id;
            return View(dishes);
        }
    }
}