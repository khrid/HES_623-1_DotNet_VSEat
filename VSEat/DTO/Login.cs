using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Login
    {
        public Login(){ }

        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string fullName { get; set; }
    }
}
