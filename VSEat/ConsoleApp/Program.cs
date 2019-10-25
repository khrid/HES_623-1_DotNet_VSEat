using BLL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        static void Main(string[] args)
        {

            CitiesManager citiesManager  = new CitiesManager(Configuration);
            List<City> myCities = citiesManager.GetAllCities();
            Console.WriteLine("CITIES");
            Console.WriteLine("----------------------------");
            for (int i = 0; i < myCities.Count; i++)
            {
                Console.WriteLine(myCities[i].ToString());
            }
            Console.WriteLine();
            Console.WriteLine("CITY");
            Console.WriteLine("----------------------------");
            Console.WriteLine(citiesManager.GetCityById(1).ToString());



            RestaurantsManager restaurantsManager = new RestaurantsManager(Configuration);
            List<Restaurant> myRestaurants = restaurantsManager.GetAllRestaurants();
            Console.WriteLine();
            Console.WriteLine("RESTAURANTS");
            Console.WriteLine("----------------------------");
            for (int i = 0; i < myRestaurants.Count; i++)
            {
                Console.WriteLine(myRestaurants[i].ToString());
            }
            Console.WriteLine();
            Console.WriteLine("RESTAURANT");
            Console.WriteLine("----------------------------");
            Console.WriteLine(restaurantsManager.GetRestaurantById(1).ToString());


        }
    }
}
