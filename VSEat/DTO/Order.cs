using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Order
    {
        public Order() { }

        public int id { get; set; }
        public DateTime delivery_time_requested { get; set; }
        public Customer customer { get; set; }
        public Deliverer deliverer { get; set; }
    }
}
