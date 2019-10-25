using System.Collections.Generic;
using DTO;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public interface IOrdersDB
    {
        IConfiguration Configuration { get; }

        Order AddOrder(Order order);
        List<Order> GetAllOrders();
        Order GetOrderById(int id);
    }
}