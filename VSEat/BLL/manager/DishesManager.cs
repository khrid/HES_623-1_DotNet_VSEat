using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using DTO;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class DishesManager : IDishesManager
    {
        private IDishesDB DishesDB { get; }

        public DishesManager(IConfiguration configuration)
        {
            DishesDB = new DishesDB(configuration);
        }

        public List<Dish> GetAllDishes()
        {
            return DishesDB.GetAllDishes();
        }

        public List<Dish> GetAllDishesForRestaurant(int id)
        {
            List<Dish> dishes = GetAllDishes();
            List<Dish> dishesForRestaurant = new List<Dish>();

            foreach (var dish in dishes)
            {
                if (dish.restaurant.id == id)
                {
                    dishesForRestaurant.Add(dish);
                }
            }

            return dishesForRestaurant;
        }

        public Dish GetDishById(int id)
        {
            return DishesDB.GetDishById(id);
        }
    }
}
