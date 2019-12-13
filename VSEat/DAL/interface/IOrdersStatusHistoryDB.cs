using System.Collections.Generic;
using DTO;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public interface IOrdersStatusHistoryDB
    {

        OrdersStatusHistory AddOrdersStatusHistory(OrdersStatusHistory ordersStatusHistory);
        List<OrdersStatusHistory> GetAllOrdersStatusHistory();
        OrdersStatusHistory GetOrdersStatusHistoryById(int id);
        OrdersStatusHistory GetCurrentOrderStatusHistoryForOrder(int id);
        List<OrdersStatusHistory> GetEveryOrderStatusHistoryForOrder(int id);
        int DeleteOrderStatusHistoryByOrderId(int id);
    }
}