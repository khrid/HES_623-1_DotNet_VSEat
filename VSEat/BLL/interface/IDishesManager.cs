using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IDishesManager
{
        List<Dish> GetAllDishes();
        List<Dish> GetAllDishesForRestaurant(int id);
        Dish GetDishById(int id);
    }
}
