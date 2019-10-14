using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Deliverer
    {
        public Deliverer() { }

        public override string ToString()
        {
            return $"ID: {id} username: {username} full_name: {full_name} city: {city.zip_code} {city.name}";
        }

        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string full_name { get; set; }
        public City city { get; set; }
    }
}
