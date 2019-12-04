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
        private IConfiguration Configuration { get; }

        public LoginController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Login login)
        {
            LoginManager loginManager = new LoginManager(Configuration);
            bool isValid = loginManager.isUserValid(login, "customer");
            if (isValid)
            {
                HttpContext.Session.SetString("loggedIn", "1");
                HttpContext.Session.SetInt32("userid", login.id);
                HttpContext.Session.SetString("username", login.username);
                HttpContext.Session.SetString("usertype", "customer");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Deliverer()
        {
            return View("Index");
        }

        [HttpPost]
        public IActionResult Deliverer(Login login)
        {
            LoginManager loginManager = new LoginManager(Configuration);
            bool isValid = loginManager.isUserValid(login, "deliverer");
            if (isValid)
            {
                HttpContext.Session.SetString("loggedIn", "1");
                HttpContext.Session.SetInt32("userid", login.id);
                HttpContext.Session.SetString("username", login.username);
                HttpContext.Session.SetString("usertype", "deliverer");
                return RedirectToAction("Index", "Deliverers");
            }
            else
            {
                return RedirectToAction("Deliverer", "Login");
            }
        }
    }
}