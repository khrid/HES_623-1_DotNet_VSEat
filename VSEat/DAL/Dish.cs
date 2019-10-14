using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Dish
    {
        public Dish() { }

        public override string ToString()
        {
            return $"ID: {id} name: {name} price: {price} status: {status} restaurant: {restaurant.merchant_name}";
        }

        public int id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public bool status { get; set; }
        public Restaurant restaurant { get; set; }
    }
}
