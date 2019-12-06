using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface ICitiesManager
    {
        List<City> GetAllCities();
        City GetCityById(int id);
    }
}
