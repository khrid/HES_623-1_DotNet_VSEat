using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebApplication.Controllers
{
    public class OrdersController : Controller
    {
        private IConfiguration Configuration { get; }

        public OrdersController(IConfiguration configuration)
        {
            Configuration = configuration;
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

            OrdersManager ordersManager = new OrdersManager(Configuration);
            CustomersManager customersManager = new CustomersManager(Configuration);
            DeliverersManager deliverersManager = new DeliverersManager(Configuration);
            DishesManager dishesManager = new DishesManager(Configuration);
            Dish dish = dishesManager.GetDishById(dishid);
            int userid = (int)HttpContext.Session.GetInt32("userid");


            //int[] order = { userid, dish.id, quantity };

            // le user veut rajouter qqch au panier, déjà qqch dedans ?
            // => orderid dans la session
            int orderid = (int)HttpContext.Session.GetInt32("orderid").GetValueOrDefault();
            System.Diagnostics.Debug.WriteLine("-----------------orderid="+orderid);
            Order order = new Order();
            if (orderid != 0) // dans ce cas on rajoute une ligne dans order_dishes
            {
                order = ordersManager.GetOrderById(orderid);
            }
            else // sinon on doit créer une commande
            {
                Customer customer = customersManager.GetCustomerById(userid);
                Deliverer deliverer = deliverersManager.GetDelivererForCity(customer.city.id);
                order = new Order { customer = customer, deliverer = deliverer, delivery_time_requested = DateTime.Now };
                order = ordersManager.AddOrder(order);
                HttpContext.Session.SetInt32("orderid", order.id);
            }

            HttpContext.Session.SetInt32("addedToCart", 1);
            OrderDishesManager orderDishesManager = new OrderDishesManager(Configuration);
            OrderDish orderDish = new OrderDish { order = order, dish = dish, quantity = quantity };
            orderDishesManager.AddOrderDish(orderDish);


            return RedirectToAction("List", "Dishes", new { id = Request.Form["restaurantId"] });
        }

        public IActionResult DisplayCart()
        {
            OrderDishesManager orderDishesManager = new OrderDishesManager(Configuration);
            OrdersManager ordersManager = new OrdersManager(Configuration);
            CustomersManager customersManager = new CustomersManager(Configuration);
            DeliverersManager deliverersManager = new DeliverersManager(Configuration);
            DishesManager dishesManager = new DishesManager(Configuration);

            int orderid = (int)HttpContext.Session.GetInt32("orderid").GetValueOrDefault();

            List<OrderDish> orderDishes = orderDishesManager.GetOrderDishByOrderId(orderid);

            return View(orderDishes);
        }
    }
}