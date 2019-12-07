using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class MyOrdersViewModel
    {
        public Deliverer deliverer { get; set; }

        public Restaurant restaurant { get; set; }

        public Customer customer { get; set; }

        public OrdersStatusHistory ordersStatusHistory { get; set; }

        public Order order { get; set; }

        public int orderAmount { get; set; }
    }
}
