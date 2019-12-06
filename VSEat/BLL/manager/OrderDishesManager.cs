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

        public OrderDishesManager(IConfiguration configuration)
        {
            OrderDishesDb = new OrderDishesDB(configuration);
        }
        public OrderDish AddOrderDish(OrderDish orderDish)
        {
            return OrderDishesDb.AddOrderDish(orderDish);
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
