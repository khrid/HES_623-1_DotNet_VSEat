using System;
using System.Collections.Generic;
using DAL;
using DTO;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class RestaurantsManager : IRestaurantsManager
    {
        public IRestaurantsDB RestaurantsDB { get; }

        public RestaurantsManager(IConfiguration configuration)
        {
            RestaurantsDB = new RestaurantsDB(configuration);
        }

        public List<Restaurant> GetAllRestaurants()
        {
            return RestaurantsDB.GetAllRestaurants();
        }

        public Restaurant GetRestaurantById(int id)
        {
            return RestaurantsDB.GetRestaurantById(id);
        }
    }
}
