using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface IDeliverersManager
    {

        List<Deliverer> GetAllDeliverers();
        int UpdateDeliverer(Deliverer deliverer);
        Deliverer GetDelivererById(int id);
        List<Deliverer> GetDeliverersForCity(int id);
        Deliverer GetTempDeliverer();

    }
}
