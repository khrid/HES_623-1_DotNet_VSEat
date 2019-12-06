using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IRestaurantsManager
    {
        List<Restaurant> GetAllRestaurants();
        Restaurant GetRestaurantById(int id);
        List<Restaurant> GetRestaurantByCity(int id);
    }
}
