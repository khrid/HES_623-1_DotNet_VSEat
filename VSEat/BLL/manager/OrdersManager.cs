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
        private IOrdersDB OrdersDB { get; }

        public OrdersManager(IOrdersDB ordersDB)
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
                if (order.deliverer.id == id)
                {
                    OrdersByDeliverer.Add(order);
                }
            }
            return OrdersByDeliverer;
        }

        public List<Order> GetOrderByCustomerId(int id)
        {
            List<Order> AllOrders = GetAllOrders();
            List<Order> OrdersByCustomer = new List<Order>();

            if (AllOrders != null)
            {
                foreach (var order in AllOrders)
                {
                    if (order.customer.id == id)
                    {
                        OrdersByCustomer.Add(order);
                    }
                }
            }

            return OrdersByCustomer;
        }

        public List<Order> GetOrdersForDelivererInTimespan(int delivererId, DateTime deliveryTime)
        {
            List<Order> OrdersInTimespan = new List<Order>();
            List<Order> OrdersByDeliverer = GetOrderByDelivererId(delivererId);

            DateTime min = deliveryTime.AddMinutes(-30);
            DateTime max = deliveryTime.AddMinutes(30);

            foreach (var order in OrdersByDeliverer)
            {
                // Si la date de commande demandée est + / - 30mn
                if (order.delivery_time_requested > min && max > order.delivery_time_requested)
                {
                    OrdersInTimespan.Add(order);
                }
            }

            return OrdersInTimespan;
        }

        public int UpdateOrder(Order order)
        {
            return OrdersDB.UpdateOrder(order);
        }
    }
}
