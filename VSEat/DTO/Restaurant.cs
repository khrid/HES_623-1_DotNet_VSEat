using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Restaurant
    {
        public Restaurant() { }

        public override string ToString()
        {
            return $"ID: {id} restaurant: {merchant_name} city: {city.zip_code} {city.name}";
        }

        public int id { get; set; }
        public string merchant_name { get; set; }
        public City city { get; set; }
    }
}
