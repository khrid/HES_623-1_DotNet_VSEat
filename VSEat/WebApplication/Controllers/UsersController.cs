﻿using System;
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
    public class UsersController : Controller
    {

        private IConfiguration Configuration { get; }

        public UsersController(IConfiguration configuration)
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

        public IActionResult Register()
        {
            CitiesManager citiesManager = new CitiesManager(Configuration);
            ViewBag.CityList = new SelectList(citiesManager.GetAllCities(), "id", "name");
            return View();
        }

        [HttpPost]
        public IActionResult Register(Customer customer)
        {
            int cityid = int.Parse(Request.Form["cities"]);
            System.Diagnostics.Debug.WriteLine("---------------" + Request.Form["cities"]);
            CitiesManager citiesManager = new CitiesManager(Configuration);

            CustomersManager customersManager = new CustomersManager(Configuration);
            System.Diagnostics.Debug.WriteLine("---------------"+customer.full_name);
            //System.Diagnostics.Debug.WriteLine("---------------" + customer.city.name);
            customer.city = citiesManager.GetCityById(cityid);
            customersManager.AddCustomer(customer);
            if(customer.id != 0)
            {
                HttpContext.Session.SetInt32("accountCreated", 1);
            }
            return RedirectToAction("Index", "Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}