﻿using System.Collections.Generic;
using DTO;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public interface IDishesDB
    {

        List<Dish> GetAllDishes();
        List<Dish> GetAllDishesForRestaurant(int id);
        Dish GetDishById(int id);
    }
}