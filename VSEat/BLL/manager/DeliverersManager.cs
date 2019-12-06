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
        private IDeliverersDB DeliverersDB { get; }

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

        public Deliverer GetDelivererForCity(int id)
        {
            List<Deliverer> AllDeliverers = GetAllDeliverers();
            List<Deliverer> DelivererInCity = new List<Deliverer>();

            foreach (var deliverer in AllDeliverers)
            {
                if (deliverer.city.id == id)
                {
                    DelivererInCity.Add(deliverer);
                }
            }

            //if(DelivererInCity.Count > 0)
            //{
            return DelivererInCity[new Random().Next(0, DelivererInCity.Count)];
            //}
        }
    }
}
