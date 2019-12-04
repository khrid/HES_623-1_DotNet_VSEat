using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Dish
    {
        public Dish() { }
        
        public int id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public bool status { get; set; }
        public Restaurant restaurant { get; set; }
    }
}
