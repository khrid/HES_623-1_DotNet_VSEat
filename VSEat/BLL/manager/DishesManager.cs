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
        public IDishesDB DishesDB { get; }

        public DishesManager(IConfiguration configuration)
        {
            DishesDB = new DishesDB(configuration);
        }

        public List<Dish> GetAllDishes()
        {
            return DishesDB.GetAllDishes();
        }

        public Dish GetDishById(int id)
        {
            return DishesDB.GetDishById(id);
        }
    }
}
