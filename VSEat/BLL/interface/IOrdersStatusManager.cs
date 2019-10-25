using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    interface IOrdersStatusManager
    {
        IOrdersStatusDB OrdersStatusDB { get; }

        List<OrdersStatus> GetAllOrdersStatus();
        OrdersStatus GetOrdersStatusById(int id);
    }
}
