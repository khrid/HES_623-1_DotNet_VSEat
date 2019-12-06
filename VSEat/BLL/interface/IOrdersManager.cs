using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IOrdersManager
    {
        IOrdersDB OrdersDB { get; }
        Order AddOrder(Order order);
        List<Order> GetAllOrders();
        Order GetOrderById(int id);
    }
}
