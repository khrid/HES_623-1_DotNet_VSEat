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
        Deliverer GetDelivererById(int id);
    }
}
