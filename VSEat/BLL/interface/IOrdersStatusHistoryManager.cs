using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IOrdersStatusHistoryManager
    {
        OrdersStatusHistory AddOrdersStatusHistory(OrdersStatusHistory ordersStatusHistory);
        List<OrdersStatusHistory> GetAllOrdersStatusHistory();
        OrdersStatusHistory GetOrdersStatusHistoryById(int id);
        OrdersStatusHistory GetCurrentOrderStatusHistoryForOrder(int id);
        List<OrdersStatusHistory> GetEveryOrderStatusHistoryForOrder(int id);
        int DeleteOrderStatusHistoryByOrderId(int id);
    }
}
