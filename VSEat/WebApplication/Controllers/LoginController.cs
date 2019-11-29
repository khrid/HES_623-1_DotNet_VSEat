using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DTO;
using DAL;
using BLL;
using Microsoft.Extensions.Configuration;

namespace WebApplication.Controllers
{
    public class LoginController : Controller
    {

        private IConfiguration Configuration { get; }

        public LoginController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
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
                return RedirectToAction(nameof(Index), "Home");
            }
            else
            {
                return View();
            }
        }
    }
}