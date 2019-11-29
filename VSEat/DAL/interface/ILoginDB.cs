using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface ILoginDB
    {
        bool IsUserValid(Login l);
    }
}
