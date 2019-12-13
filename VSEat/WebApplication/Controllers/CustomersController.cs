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
            return RedirectToAction("Index", "Home"); ;
        }

        public IActionResult Me()
        {

            if (HttpContext.Session.GetString("usertype") != "customer")
            {
                return RedirectToAction("Me", "Deliverers");
            }
            int id = (int)HttpContext.Session.GetInt32("userid");
            Customer cust = customersManager.GetCustomerById(id);
            ViewBag.CityList = new SelectList(citiesManager.GetAllCities(), "id", "name",cust.city.id);
            return View(cust);
        }

        [HttpPost]
        public IActionResult UpdateCustomerInformation()
        {
            int id = (int)HttpContext.Session.GetInt32("userid");
            Customer cust = customersManager.GetCustomerById(id);

            string oldpass = Request.Form["password"][0];
            string newpass = Request.Form["password"][1];
            string newpass2 = Request.Form["password"][2];

            if (cust.password == oldpass)
            {
                if (newpass == newpass2)
                {
                    cust.password = newpass;
                }
                else
                {
                    HttpContext.Session.SetInt32("newPasswordsDoNotMatch", 1);
                    return RedirectToAction("Me", "Customers");
                }
            }
            else if (!String.IsNullOrEmpty(oldpass))
            {
                HttpContext.Session.SetInt32("currentPasswordDoesNotMatch", 1);
                return RedirectToAction("Me", "Customers");
            }

            cust.full_name = Request.Form["full_name"];
            cust.address = Request.Form["address"];
            cust.city.id = int.Parse(Request.Form["cities"]);
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

            string newpass = Request.Form["password"][0];
            string newpass2 = Request.Form["password"][1];
            if(newpass != newpass2)
            {
                HttpContext.Session.SetInt32("newPasswordsDoNotMatch", 1);
                return RedirectToAction("Register", "Customers");
            }
            else
            {
                int cityid = int.Parse(Request.Form["cities"]);
                //System.Diagnostics.Debug.WriteLine("---------------" + Request.Form["cities"]);
                //System.Diagnostics.Debug.WriteLine("---------------" + customer.full_name);
                //System.Diagnostics.Debug.WriteLine("---------------" + customer.city.name);
                customer.city = citiesManager.GetCityById(cityid);
                customersManager.AddCustomer(customer);
                if (customer.id != 0)
                {
                    HttpContext.Session.SetInt32("accountCreated", 1);
                }
                else
                {
                    HttpContext.Session.SetInt32("creationError", 1);
                    return RedirectToAction("Register", "Customers");
                }
                return RedirectToAction("Index", "Login");
            }

            
        }
    }
}