using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    interface ICitiesManager
{
        ICitiesDB CitiesDB { get; }
        List<City> GetAllCities();
        City GetCityById(int id);
    }
}
