using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO
{
    public class Customer
    {
        public Customer() { }

        public int id { get; set; }
        [Required]
        public string username { get; set; }
        public string password { get; set; }
        [Required]
        public string full_name { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public City city { get; set; }
    }
}
