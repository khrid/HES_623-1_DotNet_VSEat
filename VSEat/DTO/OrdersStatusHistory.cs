using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class OrdersStatusHistory
    {
        public OrdersStatusHistory() { }

        public int id { get; set; }
        public DateTime created_at { get; set; }
        public Order order { get; set; }
        public OrdersStatus ordersStatus { get; set; }
    }
}
