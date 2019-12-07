using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IOrdersStatusManager
    {
        
        List<OrdersStatus> GetAllOrdersStatus();
        OrdersStatus GetOrdersStatusById(int id);
        OrdersStatus GetOrdersStatusByStatus(string status);
    }
}
