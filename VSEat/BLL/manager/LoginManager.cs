using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using DAL;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class LoginManager : ILoginManager
    {

        public ILoginDB LoginDB { get; }

        public LoginManager(IConfiguration configuration)
        {
            LoginDB = new LoginDB(configuration);
        }

        public bool isUserValid(Login login)
        {
            return LoginDB.isUserValid(login);
        }
    }
}
