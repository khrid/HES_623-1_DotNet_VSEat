using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class OrderDish
    {
        public OrderDish() { }


        public int id { get; set; }
        public int quantity { get; set; }
        public Order order { get; set; }
        public Dish dish { get; set; }
    }
}
