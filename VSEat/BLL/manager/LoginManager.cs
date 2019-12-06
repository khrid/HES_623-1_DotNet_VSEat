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

        private ILoginDB LoginDB { get; }

        public LoginManager(ILoginDB loginDB)
        {
            LoginDB = loginDB;
        }

        public bool isUserValid(Login login, string type)
        {
            return LoginDB.isUserValid(login, type);
        }

    }
}
