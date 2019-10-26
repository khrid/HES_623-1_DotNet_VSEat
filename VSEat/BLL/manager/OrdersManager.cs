using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using DTO;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class OrdersManager : IOrdersManager
    {
        public IOrdersDB OrdersDB { get; }

        public OrdersManager(IConfiguration configuration)
        {
            OrdersDB = new OrdersDB(configuration);
        }

        public Order AddOrder(Order order)
        {
            return OrdersDB.AddOrder(order);
        }

        public List<Order> GetAllOrders()
        {
            return OrdersDB.GetAllOrders();
        }

        public Order GetOrderById(int id)
        {
            Order o = OrdersDB.GetOrderById(id);
            if (o != null)
            {
                return OrdersDB.GetOrderById(id);
            }
            else
            {
                return new Order();
            }
        }
    }
}
