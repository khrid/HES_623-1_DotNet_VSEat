using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IOrderDishesManager
    {
        OrderDish AddOrderDish(OrderDish orderDish);
        int DeleteOrderDish(int id);
        List<OrderDish> GetAllOrderDishes();
        OrderDish GetOrderDishById(int id);
        List<OrderDish> GetOrderDishByOrderId(int id);
    }
}
