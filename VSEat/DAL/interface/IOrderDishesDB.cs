using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public interface IOrderDishesDB
    {
        IConfiguration Configuration { get; }

        OrderDish AddOrderDish(OrderDish orderDish);
        List<OrderDish> GetAllOrderDishes();
        OrderDish GetOrderDishById(int id);
    }
}