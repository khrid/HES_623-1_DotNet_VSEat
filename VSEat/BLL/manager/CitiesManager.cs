using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using DTO;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class CitiesManager : ICitiesManager
    {
        public ICitiesDB CitiesDB { get; }

        public CitiesManager(IConfiguration configuration)
        {
            CitiesDB = new CitiesDB(configuration);
        }

        public List<City> GetAllCities()
        {
            return CitiesDB.GetAllCities();
        }

        public City GetCityById(int id)
        {
            return CitiesDB.GetCityById(id);
        }
    }
}
