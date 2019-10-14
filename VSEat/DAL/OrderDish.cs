using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class OrderDish
    {
        public OrderDish() { }

        public override string ToString()
        {
            return $"ID: {id} order:{order.id} dish: {dish.name} quantity: {quantity}";
        }

        public int id { get; set; }
        public int quantity { get; set; }
        public Order order { get; set; }
        public Dish dish { get; set; }
    }
}
