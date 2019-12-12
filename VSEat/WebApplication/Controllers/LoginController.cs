using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace WebApplication.Controllers
{
    public class LoginController : Controller
    {
        private ILoginManager loginManager { get; }

        public LoginController(ILoginManager loginManager)
        {
            this.loginManager = loginManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Login login)
        {
            bool isValid = loginManager.isUserValid(login, "customer");
            if (isValid)
            {
                HttpContext.Session.SetString("loggedIn", "1");
                HttpContext.Session.SetInt32("userid", login.id);
                HttpContext.Session.SetString("fullname", login.fullName);
                HttpContext.Session.SetString("usertype", "customer");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                HttpContext.Session.SetInt32("loginFailed", 1);
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        public IActionResult Deliverer()
        {
            return View("Index");
        }

        [HttpPost]
        public IActionResult Deliverer(Login login)
        {
            bool isValid = loginManager.isUserValid(login, "deliverer");
            if (isValid)
            {
                HttpContext.Session.SetString("loggedIn", "1");
                HttpContext.Session.SetInt32("userid", login.id);
                HttpContext.Session.SetString("fullname", login.fullName);
                HttpContext.Session.SetString("usertype", "deliverer");
                return RedirectToAction("MyDeliveries", "Orders");
            }
            else
            {
                HttpContext.Session.SetInt32("loginFailed", 1);
                return View("Index");
            }
        }
    }
}