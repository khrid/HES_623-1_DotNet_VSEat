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
        private ICitiesDB CitiesDB { get; }

        public CitiesManager(ICitiesDB citiesDB)
        {
            CitiesDB = citiesDB;
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
