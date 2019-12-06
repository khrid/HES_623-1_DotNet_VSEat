using System.Collections.Generic;
using DTO;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public interface IOrdersStatusDB
    {

        List<OrdersStatus> GetAllOrdersStatus();
        OrdersStatus GetOrdersStatusById(int id);
    }
}