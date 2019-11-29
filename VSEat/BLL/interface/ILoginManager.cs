using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace BLL
{
    public interface ILoginManager
{
        bool isUserValid(Login login);
}
}
