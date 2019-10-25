using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    interface IOrdersStatusHistoryManager
    {

        IOrdersStatusHistoryDB OrdersStatusHistoryDB { get; }

        OrdersStatusHistory AddOrdersStatusHistory(OrdersStatusHistory ordersStatusHistory);
        List<OrdersStatusHistory> GetAllOrdersStatusHistory();
        OrdersStatusHistory GetOrdersStatusHistoryById(int id);
    }
}
