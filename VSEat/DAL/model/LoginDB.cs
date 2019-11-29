using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class LoginDB : ILoginDB
    {
        public  IConfiguration configuration { get; }
        private string connectionString = null;

        public LoginDB(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("DefaultConnection");

        }

        public bool IsUserValid(Login l)
        {
            // TODO sql query tu check if login is valid
            return true;
        }
    }
}
