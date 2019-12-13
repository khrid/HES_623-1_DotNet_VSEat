using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace WebApplication.Controllers
{
    public class CustomersController : Controller
    {

        private ICustomersManager customersManager { get; }
        private ICitiesManager citiesManager { get; }

        public CustomersController(ICustomersManager customersManager, ICitiesManager citiesManager)
        {
            this.customersManager = customersManager;
            this.citiesManager = citiesManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Me()
        {

            if (HttpContext.Session.GetString("usertype") != "customer")
            {
                return RedirectToAction("Deliverers", "Me");
            }
            int id = (int)HttpContext.Session.GetInt32("userid");
            Customer cust = customersManager.GetCustomerById(id);
            return View(cust);
        }

        [HttpPost]
        public IActionResult UpdateCustomerInformation()
        {
            int id = (int)HttpContext.Session.GetInt32("userid");
            Customer cust = customersManager.GetCustomerById(id);
            cust.password = Request.Form["password"];
            cust.full_name = Request.Form["full_name"];
            cust.address = Request.Form["address"];
            customersManager.UpdateCustomer(cust);
            HttpContext.Session.SetInt32("updatedCustomerInformation", 1);
            return RedirectToAction("Me", "Customers");
        }

        public IActionResult Register()
        {
            ViewBag.CityList = new SelectList(citiesManager.GetAllCities(), "id", "name");
            return View();
        }

        [HttpPost]
        public IActionResult Register(Customer customer)
        {
            int cityid = int.Parse(Request.Form["cities"]);
            System.Diagnostics.Debug.WriteLine("---------------" + Request.Form["cities"]);

            System.Diagnostics.Debug.WriteLine("---------------" + customer.full_name);
            //System.Diagnostics.Debug.WriteLine("---------------" + customer.city.name);
            customer.city = citiesManager.GetCityById(cityid);
            customersManager.AddCustomer(customer);
            if (customer.id != 0)
            {
                HttpContext.Session.SetInt32("accountCreated", 1);
            } else
            {
                HttpContext.Session.SetInt32("creationError", 1);
                return RedirectToAction("Register", "Customers");
            }
            return RedirectToAction("Index", "Login");
        }
    }
}