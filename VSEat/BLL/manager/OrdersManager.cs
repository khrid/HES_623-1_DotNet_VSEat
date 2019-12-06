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

        public OrdersManager(OrdersDB ordersDB)
        {
            OrdersDB = ordersDB;
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

        public List<Order> GetOrderByDelivererId(int id)
        {
            List<Order> AllOrders = GetAllOrders();
            List<Order> OrdersByDeliverer = new List<Order>();

            foreach (var order in AllOrders)
            {
                if(order.deliverer.id == id)
                {
                    OrdersByDeliverer.Add(order);
                }
            }
            System.Diagnostics.Debug.WriteLine(OrdersByDeliverer);
            return OrdersByDeliverer;
        }
    }
}
