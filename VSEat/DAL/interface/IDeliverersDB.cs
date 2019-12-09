using System.Collections.Generic;
using DTO;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public interface IDeliverersDB
    {

        List<Deliverer> GetAllDeliverers();
        Deliverer GetDelivererById(int id);
        int UpdateDeliverer(Deliverer deliverer);
    }
}