using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    interface IDeliverersManager
{
        IDeliverersDB DeliverersDB { get; }

        List<Deliverer> GetAllDeliverers();
        Deliverer GetDelivererById(int id);
    }
}
