using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DTO
{
    public class Customer
    {
        public Customer() { }

        /*public override string ToString()
        {
            return $"ID: {id} username: {username} full_name: {full_name} city: {city.zip_code} {city.name}";
        }*/

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
