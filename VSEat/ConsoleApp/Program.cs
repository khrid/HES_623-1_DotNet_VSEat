using DAL;
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

            CitiesDB citiesDB = new CitiesDB(Configuration);
            List<City> myCities = citiesDB.GetAllCities();
            Console.WriteLine("CITIES");
            Console.WriteLine("----------------------------");
            for (int i = 0; i < myCities.Count; i++)
            {
                Console.WriteLine(myCities[i].ToString());
            }
            Console.WriteLine();
            Console.WriteLine("CITY");
            Console.WriteLine("----------------------------");
            Console.WriteLine(citiesDB.GetCity(1).ToString());



            OrdersStatusDB ordersStatusDB = new OrdersStatusDB(Configuration);
            List<OrdersStatus> myordersStatus = ordersStatusDB.GetAllOrdersStatus();
            Console.WriteLine();
            Console.WriteLine("ORDERS_STATUS");
            Console.WriteLine("----------------------------");
            for (int i = 0; i < myordersStatus.Count; i++)
            {
                Console.WriteLine(myordersStatus[i].ToString());
            }



            RestaurantsDB restaurantsDB = new RestaurantsDB(Configuration);
            List<Restaurant> myRestaurants = restaurantsDB.GetAllRestaurants();
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
            Console.WriteLine(restaurantsDB.GetRestaurant(1).ToString());

        }
    }
}
