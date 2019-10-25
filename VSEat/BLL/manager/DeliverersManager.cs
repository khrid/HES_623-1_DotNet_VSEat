using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using DTO;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class DeliverersManager : IDeliverersManager
    {
        public IDeliverersDB DeliverersDB { get; }

        public DeliverersManager(IConfiguration configuration)
        {
            DeliverersDB = new DeliverersDB(configuration);
        }

        public List<Deliverer> GetAllDeliverers()
        {
            return DeliverersDB.GetAllDeliverers();
        }

        public Deliverer GetDelivererById(int id)
        {
            return DeliverersDB.GetDelivererById(id);
        }
    }
}
