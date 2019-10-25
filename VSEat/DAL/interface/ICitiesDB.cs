using DTO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DAL
{
    public interface ICitiesDB
    {
        IConfiguration Configuration { get; }
        List<City> GetAllCities();
        City GetCityById(int id);
    }
}