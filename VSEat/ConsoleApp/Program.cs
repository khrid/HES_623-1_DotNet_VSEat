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
        // Configuration pour le lien avec la BDD
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();


        // Managers
        /*
        CitiesManager citiesManager = new CitiesManager(Configuration);
        CustomersManager customersManager = new CustomersManager(Configuration);
        DeliverersManager deliverersManager = new DeliverersManager(Configuration);
        DishesManager dishesManager = new DishesManager(Configuration);
        OrderDishesManager orderDishesManager = new OrderDishesManager(Configuration);
        OrdersManager ordersManager = new OrdersManager(Configuration);
        OrdersStatusHistoryManager ordersStatusHistoryManager = new OrdersStatusHistoryManager(Configuration);
        OrdersStatusManager ordersStatusManager = new OrdersStatusManager(Configuration);
        RestaurantsManager restaurantsManager = new RestaurantsManager(Configuration);
        */

        static void Main(string[] args)
        {
            Program p = new Program();
            //p.TestEverything();
            p.SimuScenario();
        }

        public void SimuScenario()
        {
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Simulating scenario");
            Console.WriteLine("--------------------------------------------------------");
            string User = "sylvain.meyer";
            string Pwd = "Pass1234";

            // Connexion
            Console.WriteLine($"Authenticating user {User} with password {Pwd}.");
            Customer c = customersManager.CheckAuthentication(User, Pwd);
            if (c != null)
            {

                Console.WriteLine($"Successfully authenticated user {c.username}.");
                Console.WriteLine("Listing avalaible restaurants.");
                // Listing des restaurants disponibles
                Restaurant selectedRestaurant = new Restaurant();
                foreach (Restaurant r in restaurantsManager.GetAllRestaurants())
                {
                    Console.WriteLine("\t" + r.merchant_name);
                    // On sélectionne arbitrairement un restaurant pour ce scénario
                    if (r.merchant_name == "Fish from Valais")
                    {
                        selectedRestaurant = r;
                    }
                }
                Console.WriteLine("User selects \"Fish from Valais\".");
                Console.WriteLine("Listing avalaible dishes for selected restaurant.");

                // Listing des plats du restaurant choisi
                Dish selectedDish = new Dish();
                foreach (Dish d in dishesManager.GetAllDishesForRestaurant(selectedRestaurant.id))
                {
                    Console.WriteLine("\t" + d.name + " - " + d.price);
                    if (d.name == "Cheeseburger")
                    {
                        selectedDish = d;
                    }
                }

                // Création d'une nouvelle commande "vide"
                Console.WriteLine("Creating a new order");
                Order order = new Order();
                order.customer = c;
                order.deliverer = deliverersManager.GetDelivererById(2);
                order.delivery_time_requested = DateTime.Now.AddHours(4);
                order = ordersManager.AddOrder(order);
                Console.WriteLine($"Created order id {order.id}");

                // On ajoute les plats voulus à la commande
                Console.WriteLine("Adding dishes to the order");
                OrderDish orderDish = new OrderDish();
                orderDish.order = order;
                orderDish.dish = selectedDish;
                orderDish.quantity = 2;
                orderDish = orderDishesManager.AddOrderDish(orderDish);
                Console.WriteLine($"Added {orderDish.quantity} {orderDish.dish.name} to the order");

                int total = 0;
                foreach (var od in orderDishesManager.GetOrderDishByOrderId(orderDish.order.id))
                {
                    total += od.dish.price * od.quantity;
                }
                Console.WriteLine($"Total amount : {total}");

                Console.WriteLine("Adding history info for the order");
                OrdersStatusHistory ordersStatusHistory = new OrdersStatusHistory();
                ordersStatusHistory.order = orderDish.order;
                ordersStatusHistory.created_at = DateTime.Now; // TODO à voir si on peut le mettre par défaut au lieu de le rajouter
                ordersStatusHistory.ordersStatus = ordersStatusManager.GetOrdersStatusById(1);
                ordersStatusHistory = ordersStatusHistoryManager.AddOrdersStatusHistory(ordersStatusHistory);
                Console.WriteLine("Current order status : " + ordersStatusHistoryManager.GetCurrentOrderStatusHistoryForOrder(order.id).ordersStatus.status);


            }
            else
            {
                Console.WriteLine($"Could not authenticate user {User}.");
            }

            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("End of simulation");
            Console.WriteLine("--------------------------------------------------------");
        }


        public void TestEverything()
        {
            Console.WriteLine("Testing everything...");
            Console.WriteLine("--------------------------------------------------------");

            /*
             * CITIES
             */
            Console.WriteLine("CITIES");
            Console.WriteLine("----------------------------");
            List<City> myCities = citiesManager.GetAllCities();
            for (int i = 0; i < myCities.Count; i++)
            {
                Console.WriteLine(myCities[i].ToString());
            }
            Console.WriteLine();
            Console.WriteLine("CITY");
            Console.WriteLine("----------------------------");
            Console.WriteLine(citiesManager.GetCityById(1).ToString());

            /*
             * Customers
             */
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

            /*
             * Deliverers
             */
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

            /*
             * Dishes
             */
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

            /*
             * OrderDishes
             */
            List<OrderDish> myOrderDishes = orderDishesManager.GetAllOrderDishes();
            Console.WriteLine();
            Console.WriteLine("ORDERDISHES");
            Console.WriteLine("----------------------------");
            for (int i = 0; i < myOrderDishes.Count; i++)
            {
                Console.WriteLine(myOrderDishes[i].ToString());
            }
            Console.WriteLine();
            Console.WriteLine("ORDERDISH");
            Console.WriteLine("----------------------------");
            Console.WriteLine(orderDishesManager.GetOrderDishById(1).ToString());

            /*
             * Orders
             */
            List<Order> myOrders = ordersManager.GetAllOrders();
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
            Console.WriteLine(ordersManager.GetOrderById(2).ToString());

            /*
             * OrdersStatus
             */
            List<OrdersStatus> myOrdersStatus = ordersStatusManager.GetAllOrdersStatus();
            Console.WriteLine();
            Console.WriteLine("ORDERSSTATUSES");
            Console.WriteLine("----------------------------");
            for (int i = 0; i < myOrdersStatus.Count; i++)
            {
                Console.WriteLine(myOrdersStatus[i].ToString());
            }
            Console.WriteLine();
            Console.WriteLine("ORDERSSTATUS");
            Console.WriteLine("----------------------------");
            Console.WriteLine(ordersStatusManager.GetOrdersStatusById(1).ToString());

            /*
             * OrdersStatusHistory
             */
            List<OrdersStatusHistory> myOrdersStatusHistory = ordersStatusHistoryManager.GetAllOrdersStatusHistory();
            Console.WriteLine();
            Console.WriteLine("ORDERSSTATUSHISTORIES");
            Console.WriteLine("----------------------------");
            for (int i = 0; i < myOrdersStatusHistory.Count; i++)
            {
                Console.WriteLine(myOrdersStatusHistory[i].ToString());
            }
            Console.WriteLine();
            Console.WriteLine("ORDERSSTATUSHISTORY");
            Console.WriteLine("----------------------------");
            Console.WriteLine(ordersStatusHistoryManager.GetOrdersStatusHistoryById(1).ToString());


            /*
             * Restaurants
             */
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
