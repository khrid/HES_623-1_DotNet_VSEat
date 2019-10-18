using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public interface IDishesDB
    {
        IConfiguration Configuration { get; }

        List<Dish> GetAllDishes();
        Dish GetDishById(int id);
    }
}