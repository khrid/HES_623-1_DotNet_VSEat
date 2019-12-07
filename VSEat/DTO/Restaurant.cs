using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Restaurant
    {
        public Restaurant() { }


        public int id { get; set; }
        public string merchant_name { get; set; }
        public City city { get; set; }
    }
}
