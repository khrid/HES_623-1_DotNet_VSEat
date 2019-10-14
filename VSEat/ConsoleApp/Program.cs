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
            Console.WriteLine(citiesDB.GetCityById(1).ToString());



            OrdersStatusDB ordersStatusDB = new OrdersStatusDB(Configuration);
            List<OrdersStatus> myordersStatus = ordersStatusDB.GetAllOrdersStatus();
            Console.WriteLine();
            Console.WriteLine("ORDERS_STATUS");
            Console.WriteLine("----------------------------");
            for (int i = 0; i < myordersStatus.Count; i++)
            {
                Console.WriteLine(myordersStatus[i].ToString());
            }
            Console.WriteLine();
            Console.WriteLine("ORDERS_STATUS");
            Console.WriteLine("----------------------------");
            Console.WriteLine(ordersStatusDB.GetOrdersStatusById(1).ToString());



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
            Console.WriteLine(restaurantsDB.GetRestaurantById(1).ToString());




            CustomersDB customerDB = new CustomersDB(Configuration);
            List<Customer> myCustomers = customerDB.GetAllCustomers();
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
            Console.WriteLine(customerDB.GetCustomerById(1).ToString());



            DeliverersDB deliverersDB = new DeliverersDB(Configuration);
            List<Deliverer> myDeliverers = deliverersDB.GetAllDeliverers();
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
            Console.WriteLine(deliverersDB.GetDelivererById(1).ToString());



            DishesDB dishesDB = new DishesDB(Configuration);
            List<Dish> myDishes = dishesDB.GetAllDishes();
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
            Console.WriteLine(dishesDB.GetDishById(1).ToString());



            OrdersDB ordersDB = new OrdersDB(Configuration);
            List<Order> myOrders = ordersDB.GetAllOrders();
            Console.WriteLine();
            Console.WriteLine("ORDERS");
            Console.WriteLine("----------------------------");
            for (int i = 0; i < myOrders.Count; i++)
            {
                Console.WriteLine(myOrders[i].ToString());
            }
            Console.WriteLine();
            Console.WriteLine("ORDER");
            Console.WriteLine("----------------------------");
            Console.WriteLine(ordersDB.GetOrderById(2).ToString());



            OrderDishesDB orderDishesDB = new OrderDishesDB(Configuration);
            List<OrderDish> myOrderDishes = orderDishesDB.GetAllOrderDishes();
            Console.WriteLine();
            Console.WriteLine("ORDER_DISHES");
            Console.WriteLine("----------------------------");
            for (int i = 0; i < myOrderDishes.Count; i++)
            {
                Console.WriteLine(myOrderDishes[i].ToString());
            }
            Console.WriteLine();
            Console.WriteLine("ORDER_DISH");
            Console.WriteLine("----------------------------");
            Console.WriteLine(orderDishesDB.GetOrderById(2).ToString());



            OrdersStatusHistoryDB ordersStatusHistoryDB = new OrdersStatusHistoryDB(Configuration);
            List<OrdersStatusHistory> myordersStatusHistory = ordersStatusHistoryDB.GetAllOrdersStatusHistory();
            Console.WriteLine();
            Console.WriteLine("ORDER_STATUS_HISTORY");
            Console.WriteLine("----------------------------");
            for (int i = 0; i < myordersStatusHistory.Count; i++)
            {
                Console.WriteLine(myordersStatusHistory[i].ToString());
            }
            Console.WriteLine();
            Console.WriteLine("ORDER_STATUS_HISTORY");
            Console.WriteLine("----------------------------");
            Console.WriteLine(ordersStatusHistoryDB.GetOrdersStatusHistoryById(1).ToString());

        }
    }
}
