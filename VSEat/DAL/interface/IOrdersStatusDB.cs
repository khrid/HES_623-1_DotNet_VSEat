using System.Collections.Generic;
using DTO;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public interface IOrdersStatusDB
    {
        IConfiguration Configuration { get; }

        List<OrdersStatus> GetAllOrdersStatus();
        OrdersStatus GetOrdersStatusById(int id);
    }
}