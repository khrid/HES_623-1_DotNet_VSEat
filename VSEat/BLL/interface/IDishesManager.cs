using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    interface IDishesManager
{
        IDishesDB DishesDB { get; }
        List<Dish> GetAllDishes();
        Dish GetDishById(int id);
    }
}
