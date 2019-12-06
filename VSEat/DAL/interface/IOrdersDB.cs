using System.Collections.Generic;
using DTO;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public interface IOrdersDB
    {

        Order AddOrder(Order order);
        List<Order> GetAllOrders();
        Order GetOrderById(int id);
    }
}