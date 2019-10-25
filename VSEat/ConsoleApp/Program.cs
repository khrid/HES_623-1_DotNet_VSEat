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



            CustomersManager customersManager = new CustomersManager(Configuration);
            List<Customer> myCustomers = customersManager.GetAllCustomers();
            Console.WriteLine();
            Console.WriteLine("CUSTOMERS");
            Console.WriteLine("----------------------------");
            for (int i = 0; i < myCustomers.Count; i++)
            {
                Console.WriteLine(myCustomers[i].ToString());
            }
            Console.WriteLine();
            Console.WriteLine("CUSTOMER");
            Console.WriteLine("----------------------------");
            Console.WriteLine(customersManager.GetCustomerById(1).ToString());



            DeliverersManager deliverersManager = new DeliverersManager(Configuration);
            List<Deliverer> myDeliverers = deliverersManager.GetAllDeliverers();
            Console.WriteLine();
            Console.WriteLine("DELIVERERS");
            Console.WriteLine("----------------------------");
            for (int i = 0; i < myDeliverers.Count; i++)
            {
                Console.WriteLine(myDeliverers[i].ToString());
            }
            Console.WriteLine();
            Console.WriteLine("DELIVERER");
            Console.WriteLine("----------------------------");
            Console.WriteLine(deliverersManager.GetDelivererById(1).ToString());



            DishesManager dishesManager = new DishesManager(Configuration);
            List<Dish> myDishes = dishesManager.GetAllDishes();
            Console.WriteLine();
            Console.WriteLine("DISHES");
            Console.WriteLine("----------------------------");
            for (int i = 0; i < myDishes.Count; i++)
            {
                Console.WriteLine(myDishes[i].ToString());
            }
            Console.WriteLine();
            Console.WriteLine("DISH");
            Console.WriteLine("----------------------------");
            Console.WriteLine(dishesManager.GetDishById(1).ToString());


        }
    }
}
