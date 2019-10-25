using System.Collections.Generic;
using DTO;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public interface IOrdersStatusHistoryDB
    {
        IConfiguration Configuration { get; }

        OrdersStatusHistory AddOrdersStatusHistory(OrdersStatusHistory ordersStatusHistory);
        List<OrdersStatusHistory> GetAllOrdersStatusHistory();
        OrdersStatusHistory GetOrdersStatusHistoryById(int id);
    }
}