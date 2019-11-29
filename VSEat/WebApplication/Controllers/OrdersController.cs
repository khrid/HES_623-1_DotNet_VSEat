using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using DTO;
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
        public IActionResult AddToCart(Dish dish)
        {
            System.Diagnostics.Debug.WriteLine(Request.Form["qty"]);
            int quantity = int.Parse(Request.Form["qty"]);

            OrdersManager ordersManager = new OrdersManager(Configuration);
            CustomersManager customersManager = new CustomersManager(Configuration);
            Customer customer = customersManager.GetCustomerById(1);
            Order order = new Order { customer = customer, };
            //ordersManager.AddOrder();
            return View();
        }
    }
}