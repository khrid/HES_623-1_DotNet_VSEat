using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class OrdersController : Controller
    {
        private IOrdersManager ordersManager { get; }
        private ICustomersManager customersManager { get; }
        private IDeliverersManager deliverersManager { get; }
        private IDishesManager dishesManager { get; }
        private IOrderDishesManager orderDishesManager { get; }
        private IOrdersStatusHistoryManager ordersStatusHistoryManager { get; }
        private IOrdersStatusManager ordersStatusManager { get; }

        private int MAX_ORDER_PER_DELIVERER = 5;

        public OrdersController(IOrdersManager ordersManager, ICustomersManager customersManager,
            IDeliverersManager deliverersManager, IDishesManager dishesManager, IOrderDishesManager orderDishesManager,
            IOrdersStatusHistoryManager ordersStatusHistoryManager, IOrdersStatusManager ordersStatusManager)
        {
            this.ordersManager = ordersManager;
            this.customersManager = customersManager;
            this.deliverersManager = deliverersManager;
            this.dishesManager = dishesManager;
            this.orderDishesManager = orderDishesManager;
            this.ordersStatusHistoryManager = ordersStatusHistoryManager;
            this.ordersStatusManager = ordersStatusManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart()
        {
            //System.Diagnostics.Debug.WriteLine(Request.Form["qty"]);
            int quantity = int.Parse(Request.Form["qty"]);
            int dishid = int.Parse(Request.Form["dishId"]);
            Dish dish = dishesManager.GetDishById(dishid);
            int userid = (int)HttpContext.Session.GetInt32("userid");


            //int[] order = { userid, dish.id, quantity };

            // le user veut rajouter qqch au panier, déjà qqch dedans ?
            // => orderid dans la session
            int orderid = (int)HttpContext.Session.GetInt32("orderid").GetValueOrDefault();
            System.Diagnostics.Debug.WriteLine("-----------------orderid=" + orderid);
            Order order = new Order();
            if (orderid != 0) // dans ce cas on rajoute une ligne dans order_dishes
            {
                order = ordersManager.GetOrderById(orderid);
            }
            else // sinon on doit créer une commande
            {
                Customer customer = customersManager.GetCustomerById(userid);
                //Deliverer deliverer = deliverersManager.GetDelivererForCity(customer.city.id);
                // On assigne un livreur temporaire à la commande
                Deliverer deliverer = deliverersManager.GetTempDeliverer();
                order = new Order { customer = customer, deliverer = deliverer, delivery_time_requested = DateTime.Now };
                order = ordersManager.AddOrder(order);
                HttpContext.Session.SetInt32("orderid", order.id);
            }

            HttpContext.Session.SetInt32("addedToCart", 1);
            OrderDish orderDish = new OrderDish { order = order, dish = dish, quantity = quantity };
            orderDishesManager.AddOrderDish(orderDish);


            return RedirectToAction("List", "Dishes", new { id = Request.Form["restaurantId"] });
        }

        public IActionResult DisplayCart()
        {

            int orderid = (int)HttpContext.Session.GetInt32("orderid").GetValueOrDefault();

            List<OrderDish> orderDishes = orderDishesManager.GetOrderDishByOrderId(orderid);

            return View(orderDishes);
        }

        [HttpPost]
        public IActionResult Confirm()
        {

            int orderid = (int)HttpContext.Session.GetInt32("orderid").GetValueOrDefault();
            int hours = int.Parse(Request.Form["livraisonHeures"]);
            int minutes = int.Parse(Request.Form["livraisonMinutes"]);

            //System.Diagnostics.Debug.WriteLine("---------------> hours = " + hours);
            //System.Diagnostics.Debug.WriteLine("---------------> minutes = " + minutes);
            DateTime deliveryTime = new DateTime(
                        DateTime.Now.Year,
                        DateTime.Now.Month,
                        DateTime.Now.Day,
                        hours,
                        minutes,
                        0,
                        0,
                        DateTime.Now.Kind);

            Order order = ordersManager.GetOrderById(orderid);

            // On doit retrouver le restaurant de la commande pour savoir la ville où
            // chercher un livreur
            List<OrderDish> orderDishes = orderDishesManager.GetOrderDishByOrderId(order.id);
            int cityId = orderDishes[0].dish.restaurant.city.id;
            //System.Diagnostics.Debug.WriteLine("---------------> cityid = " + cityId);
            // On doit trouver un livreur dispo pour cette commande
            // critères : 1. la ville (le livreur doit être dans la même)
            //            2. la date de livraison souhaitée (le livreur ne doit pas avoir 
            //                plus de 5 commandes à livrer dans les 30mn
            int count = 0;
            List<Deliverer> deliverers = deliverersManager.GetDeliverersForCity(cityId);
            bool canOrder = false;
            Deliverer finalDeliverer = new Deliverer();
            if (deliverers.Count > 0) // si 0, c'est qu'on a pas pu trouver de livreur 
            {
                foreach (var deliverer in deliverers)
                {
                    List<Order> orders = ordersManager.GetOrdersForDelivererInTimespan(deliverer.id, deliveryTime);

                    if (orders.Count > 0)
                    {
                        foreach (var o in orders)
                        {
                            OrdersStatusHistory ordersStatusHistory = ordersStatusHistoryManager.GetCurrentOrderStatusHistoryForOrder(o.id);
                            if (ordersStatusHistory.ordersStatus.status == OrdersStatusManager.COMMANDE_RECUE ||
                                ordersStatusHistory.ordersStatus.status == OrdersStatusManager.COMMANDE_PREPA ||
                                ordersStatusHistory.ordersStatus.status == OrdersStatusManager.COMMANDE_LIVRAISON)
                            {
                                count++;
                            }
                        }

                        if (count >= MAX_ORDER_PER_DELIVERER)
                        {
                            HttpContext.Session.SetString("orderError", "Tout les livreurs sont occupés !");
                            canOrder = false;
                        }
                        else
                        {
                            HttpContext.Session.SetString("orderError", "");
                            finalDeliverer = deliverer;
                            canOrder = true;
                        }
                    }
                    else
                    {
                        HttpContext.Session.SetString("orderError", "");
                        finalDeliverer = deliverer;
                        canOrder = true;
                    }
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("---------------> counta = " + count);
                HttpContext.Session.SetString("orderError", "Pas de livreurs pour cette ville !");
                canOrder = false;
            }

            if (canOrder)
            {
                System.Diagnostics.Debug.WriteLine("---------------> deliverer name = " + finalDeliverer.full_name);
                System.Diagnostics.Debug.WriteLine("---------------> deliveryTime = " + deliveryTime.ToString());
                order.deliverer = finalDeliverer;
                order.delivery_time_requested = deliveryTime;
                ordersManager.UpdateOrder(order);
                OrdersStatus orderStatus = ordersStatusManager.GetOrdersStatusByStatus(OrdersStatusManager.COMMANDE_RECUE);
                OrdersStatusHistory ordersStatusHistory = new OrdersStatusHistory { order = order, created_at = new DateTime(), ordersStatus = orderStatus };
                ordersStatusHistoryManager.AddOrdersStatusHistory(ordersStatusHistory);

                // On considère que la commande est directement en cours de livraison, on fait progresser les statuts
                // commande reçue -> en cours de prépa
                orderStatus = ordersStatusManager.GetOrdersStatusByStatus(OrdersStatusManager.COMMANDE_PREPA);
                ordersStatusHistory = new OrdersStatusHistory { order = order, created_at = new DateTime(), ordersStatus = orderStatus };
                ordersStatusHistoryManager.AddOrdersStatusHistory(ordersStatusHistory);
                // en cours de prépa -> en cours de livraison
                orderStatus = ordersStatusManager.GetOrdersStatusByStatus(OrdersStatusManager.COMMANDE_LIVRAISON);
                ordersStatusHistory = new OrdersStatusHistory { order = order, created_at = new DateTime(), ordersStatus = orderStatus };
                ordersStatusHistoryManager.AddOrdersStatusHistory(ordersStatusHistory);

                HttpContext.Session.Remove("orderid");

                return RedirectToAction("MyOrders", "Orders");
            }
            else
            {
                return RedirectToAction("DisplayCart", "Orders");
            }

        }

        public IActionResult MyOrders()
        {
            int userid = (int)HttpContext.Session.GetInt32("userid");

            List<Order> orders = ordersManager.GetOrderByCustomerId(userid);

            List<MyOrdersViewModel> myOrdersViewModels = new List<MyOrdersViewModel>();

            foreach (var item in orders)
            {
                MyOrdersViewModel m = new MyOrdersViewModel();
                List<OrderDish> dishes = orderDishesManager.GetOrderDishByOrderId(item.id);
                m.ordersStatusHistory = ordersStatusHistoryManager.GetCurrentOrderStatusHistoryForOrder(item.id);
                m.restaurant = dishes[0].dish.restaurant;
                m.deliverer = item.deliverer;
                m.customer = item.customer;
                m.order = item;

                foreach (var dish in dishes)
                {
                    m.orderAmount += dish.quantity * dish.dish.price;
                }

                myOrdersViewModels.Add(m);
            }


            return View(myOrdersViewModels);
        }

        public IActionResult MyDeliveries()
        {
            int userid = (int)HttpContext.Session.GetInt32("userid");
            List<Order> orders = ordersManager.GetOrderByDelivererId(userid);
            List<MyOrdersViewModel> myOrdersViewModels = new List<MyOrdersViewModel>();

            foreach (var item in orders)
            {
                MyOrdersViewModel m = new MyOrdersViewModel();
                List<OrderDish> dishes = orderDishesManager.GetOrderDishByOrderId(item.id);
                m.ordersStatusHistory = ordersStatusHistoryManager.GetCurrentOrderStatusHistoryForOrder(item.id);
                m.restaurant = dishes[0].dish.restaurant;
                m.deliverer = item.deliverer;
                m.customer = item.customer;
                m.order = item;

                foreach (var dish in dishes)
                {
                    m.orderAmount += dish.quantity * dish.dish.price;
                }

                myOrdersViewModels.Add(m);
            }

            return View(myOrdersViewModels);
        }

        [HttpGet]
        public IActionResult DeleteItem(int id)
        {
            orderDishesManager.DeleteOrderDish(id);
            HttpContext.Session.SetInt32("deletedItem", 1);
            return RedirectToAction("DisplayCart", "Orders");
        }

        public IActionResult OrderDetails(int id)
        {
            Order order = ordersManager.GetOrderById(id);
            OrderDetailsViewModel orderDetailsViewModel = new OrderDetailsViewModel();

            orderDetailsViewModel.order = order;
            orderDetailsViewModel.orderDishes = orderDishesManager.GetOrderDishByOrderId(order.id);
            orderDetailsViewModel.ordersStatusCompleteHistory = ordersStatusHistoryManager.GetEveryOrderStatusHistoryForOrder(order.id);
            orderDetailsViewModel.restaurant = orderDetailsViewModel.orderDishes[0].dish.restaurant;
            orderDetailsViewModel.deliverer = order.deliverer;
            orderDetailsViewModel.customer = order.customer;

            foreach (var dish in orderDetailsViewModel.orderDishes)
            {
                orderDetailsViewModel.orderAmount += dish.quantity * dish.dish.price;
            }

            return View(orderDetailsViewModel);
        }

        [HttpGet]
        public IActionResult Deliver(int id)
        {
            Order order = ordersManager.GetOrderById(id);
            return View(order);
        }

        [HttpPost]
        public IActionResult Deliver()
        {
            int orderid = int.Parse(Request.Form["orderid"]);
            Order order = ordersManager.GetOrderById(orderid);
            OrdersStatus orderStatus = ordersStatusManager.GetOrdersStatusByStatus(OrdersStatusManager.COMMANDE_LIVREE);
            OrdersStatusHistory ordersStatusHistory = new OrdersStatusHistory { order = order, created_at = new DateTime(), ordersStatus = orderStatus };
            ordersStatusHistoryManager.AddOrdersStatusHistory(ordersStatusHistory);
            HttpContext.Session.SetInt32("deliveredOrder", 1);
            return RedirectToAction("MyDeliveries", "Orders");
        }


        [HttpGet]
        public IActionResult Cancel(int id)
        {
            Order order = ordersManager.GetOrderById(id);
            return View(order);
        }


        [HttpPost]
        public IActionResult Cancel()
        {
            string nom = Request.Form["nom"];
            string prenom = Request.Form["prenom"];
            int orderid = int.Parse(Request.Form["orderid"]);
            Order order = ordersManager.GetOrderById(orderid);

            // on compare le nom et le prénom de la commande avec ceux saisis
            if (order.customer.full_name.ToLower() == (prenom + " " + nom).ToLower())
            {
                OrdersStatus orderStatus = ordersStatusManager.GetOrdersStatusByStatus(OrdersStatusManager.COMMANDE_ANNULEE);
                OrdersStatusHistory ordersStatusHistory = new OrdersStatusHistory { order = order, created_at = new DateTime(), ordersStatus = orderStatus };
                ordersStatusHistoryManager.AddOrdersStatusHistory(ordersStatusHistory);
                HttpContext.Session.SetInt32("deletedOrder", 1);
                return RedirectToAction("MyOrders", "Orders");
            }
            else
            {
                HttpContext.Session.SetInt32("wrongInfos", 1);
                return RedirectToAction("Cancel", "Orders");
            }

        }
    }
}