using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using DTO;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class OrderDishesManager : IOrderDishesManager
    {
        private IOrderDishesDB OrderDishesDb { get; }

        public OrderDishesManager(IOrderDishesDB orderDishesDb)
        {
            OrderDishesDb = orderDishesDb;
        }
        public OrderDish AddOrderDish(OrderDish orderDish)
        {
            return OrderDishesDb.AddOrderDish(orderDish);
        }

        public int DeleteOrderDish(int id)
        {
            return OrderDishesDb.DeleteOrderDish(id);
        }

        public List<OrderDish> GetAllOrderDishes()
        {
            return OrderDishesDb.GetAllOrderDishes();
        }

        public OrderDish GetOrderDishById(int id)
        {
            return OrderDishesDb.GetOrderDishById(id);
        }

        public List<OrderDish> GetOrderDishByOrderId(int id)
        {
            return OrderDishesDb.GetOrderDishByOrderId(id);
        }
    }
}
