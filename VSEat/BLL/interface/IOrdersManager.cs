using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IOrdersManager
    {
        Order AddOrder(Order order);
        List<Order> GetAllOrders();
        Order GetOrderById(int id);
        List<Order> GetOrderByDelivererId(int id);
        List<Order> GetOrderByCustomerId(int id);
        List<Order> GetOrdersForDelivererInTimespan(int delivererId, DateTime deliveryTime);
        int UpdateOrder(Order order);
        int DeleteOrderById(int id);
    }
}
