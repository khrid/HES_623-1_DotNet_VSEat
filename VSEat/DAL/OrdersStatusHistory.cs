using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class OrdersStatusHistory
    {
        public OrdersStatusHistory() { }

        public override string ToString()
        {
            return $"ID: {id} order:{order.id} status: {ordersStatus.status} created_at: {created_at}";
        }

        public int id { get; set; }
        public DateTime created_at { get; set; }
        public Order order { get; set; }
        public OrdersStatus ordersStatus { get; set; }
    }
}
