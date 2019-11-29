using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using DTO;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class LoginManager : ILoginManager
    {
        public ILoginDB LoginDBObject { get; }

        public LoginManager(IConfiguration configuration)
        {
            LoginDBObject = new LoginDB(configuration);
        }

        public bool IsUserValid(Login l)
        {
            return LoginDBObject.IsUserValid(l);
        }
    }
}
