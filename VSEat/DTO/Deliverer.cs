using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Deliverer
    {
        public Deliverer() { }

        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string full_name { get; set; }
        public City city { get; set; }
    }
}
