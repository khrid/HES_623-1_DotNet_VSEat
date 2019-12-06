using System;
using System.Collections.Generic;
using DAL;
using DTO;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class RestaurantsManager : IRestaurantsManager
    {
        private IRestaurantsDB RestaurantsDB { get; }

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

        public List<Restaurant> GetRestaurantByCity(int id)
        {
            List<Restaurant> restaurants = GetAllRestaurants();
            List<Restaurant> restaurantsForCity = new List<Restaurant>();

            foreach (var restaurant in restaurants)
            {
                if(restaurant.city.id == id)
                {
                    restaurantsForCity.Add(restaurant);
                }
            }

            return restaurantsForCity;
        }
    }
}
