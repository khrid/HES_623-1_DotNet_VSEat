using System.Collections.Generic;
using DTO;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public interface IOrderDishesDB
    {

        OrderDish AddOrderDish(OrderDish orderDish);
        int DeleteOrderDish(int id);
        List<OrderDish> GetAllOrderDishes();
        OrderDish GetOrderDishById(int id);
        List<OrderDish> GetOrderDishByOrderId(int id);
    }
}