using System.Collections.Generic;
using DTO;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public interface IRestaurantsDB
    {
        List<Restaurant> GetAllRestaurants();
        Restaurant GetRestaurantById(int id);
    }
}