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
    public class UserController : Controller
    {

        private IConfiguration Configuration { get; }

        public UserController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Me()
        {
            CustomersManager customersManager = new CustomersManager(Configuration);
            int id = (int)HttpContext.Session.GetInt32("userid");
            Customer cust = customersManager.GetCustomerById(id);
            return View(cust);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}