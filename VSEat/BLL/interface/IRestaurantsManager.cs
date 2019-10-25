using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    interface IRestaurantsManager
{
        IRestaurantsDB RestaurantsDB { get; }
        List<Restaurant> GetAllRestaurants();
        Restaurant GetRestaurantById(int id);
    }
}
