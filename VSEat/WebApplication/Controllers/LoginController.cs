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
            bool isValid = loginManager.isUserValid(login);
            if (isValid)
            {
                HttpContext.Session.SetString("loggedIn", "1");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}