using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using DTO;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class OrdersStatusManager : IOrdersStatusManager
    {
        private IOrdersStatusDB OrdersStatusDB { get; }

        public OrdersStatusManager(IConfiguration configuration)
        {
            OrdersStatusDB = new OrdersStatusDB(configuration);
        }

        public List<OrdersStatus> GetAllOrdersStatus()
        {
            return OrdersStatusDB.GetAllOrdersStatus();
        }

        public OrdersStatus GetOrdersStatusById(int id)
        {
            return OrdersStatusDB.GetOrdersStatusById(id);
        }
    }
}
