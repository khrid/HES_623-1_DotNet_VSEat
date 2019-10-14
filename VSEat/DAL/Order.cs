using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Order
    {
        public Order() { }

        public override string ToString()
        {
            return $"ID: {id} customer: {customer.full_name} deliverer: {deliverer.full_name} delivery_time_requested: {delivery_time_requested}";
        }

        public int id { get; set; }
        public DateTime delivery_time_requested { get; set; }
        public Customer customer { get; set; }
        public Deliverer deliverer { get; set; }
    }
}
