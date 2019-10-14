using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class City
    {
        public City() { }

        public override string ToString()
        {
            return $"ID: {id} city: {zip_code} {name}";
        }

        public int id { get; set; }
        public int zip_code { get; set; }
        public string name { get; set; }
    }
}
