using System.Collections.Generic;
using DTO;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public interface IDeliverersDB
    {
        IConfiguration Configuration { get; }

        List<Deliverer> GetAllDeliverers();
        Deliverer GetDelivererById(int id);
    }
}