using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    interface IOrderDishesManager
    {

        IOrderDishesDB OrderDishesDb{ get; }

        OrderDish AddOrderDish(OrderDish orderDish);
        List<OrderDish> GetAllOrderDishes();
        OrderDish GetOrderDishById(int id);
    }
}
