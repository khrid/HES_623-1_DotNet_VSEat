using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using DTO;

namespace WebApplication.Controllers
{
    public class DeliverersController : Controller
    {
        private IDeliverersManager deliverersManager { get; }
        private IOrdersManager ordersManager { get; }

        public DeliverersController(IDeliverersManager deliverersManager, IOrdersManager ordersManager)
        {
            this.deliverersManager = deliverersManager;
            this.ordersManager = ordersManager;
        }

        public IActionResult Me()
        {
            int id = (int)HttpContext.Session.GetInt32("userid");
            Deliverer deliverer = deliverersManager.GetDelivererById(id);
            return View(deliverer);
        }

        public IActionResult UpdateDelivererInformation()
        {
            int id = (int)HttpContext.Session.GetInt32("userid");
            Deliverer deliverer = deliverersManager.GetDelivererById(id);
            deliverer.password = Request.Form["password"];
            deliverer.full_name = Request.Form["full_name"];
            //cust.address = Request.Form["address"];
            deliverersManager.UpdateDeliverer(deliverer);
            HttpContext.Session.SetInt32("updatedDelivererInformation", 1);
            return RedirectToAction("Me", "Deliverers");
        }

        public IActionResult Index()
        {
            /*OrdersManager ordersManager = new OrdersManager(Configuration);
            int id = (int)HttpContext.Session.GetInt32("userid");
            List<Order> list = ordersManager.GetOrderByDelivererId(1);
            return View(list);*/
            return View();
        }

    }
}