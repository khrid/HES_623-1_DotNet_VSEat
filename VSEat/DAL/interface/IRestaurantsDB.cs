using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public interface IRestaurantsDB
    {
        IConfiguration Configuration { get; }

        List<Restaurant> GetAllRestaurants();
        Restaurant GetRestaurantById(int id);
    }
}