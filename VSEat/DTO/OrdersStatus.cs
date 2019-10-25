using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class OrdersStatus
    {
        public OrdersStatus() { }

        public override string ToString()
        {
            return $"ID: {id} order_status: {status}";
        }

        public int id { get; set; }
        public string status { get; set; }
    }
}
