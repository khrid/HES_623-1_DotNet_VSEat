using System;
using System.Collections.Generic;
using System.Text;
using DTO;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public interface ILoginDB
{
        IConfiguration Configuration { get; }
        bool isUserValid(Login login);

        // add sql to get user data

}
}
