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

        const string TEMP_DELIVERER_NAME = "temp_deliverer";

        public DeliverersManager(IDeliverersDB deliverersDB)
        {
            DeliverersDB = deliverersDB;
        }

        public int UpdateDeliverer(Deliverer deliverer)
        {
            return DeliverersDB.UpdateDeliverer(deliverer);
        }

        public List<Deliverer> GetAllDeliverers()
        {
            return DeliverersDB.GetAllDeliverers();
        }

        public Deliverer GetDelivererById(int id)
        {
            return DeliverersDB.GetDelivererById(id);
        }

        public List<Deliverer> GetDeliverersForCity(int id)
        {
            List<Deliverer> AllDeliverers = GetAllDeliverers();
            List<Deliverer> DeliverersInCity = new List<Deliverer>();

            foreach (var deliverer in AllDeliverers)
            {
                if (deliverer.city.id == id && deliverer.full_name != TEMP_DELIVERER_NAME)
                {
                    DeliverersInCity.Add(deliverer);
                }
            }

            return DeliverersInCity;
        }

        public Deliverer GetTempDeliverer()
        {
            List<Deliverer> AllDeliverers = GetAllDeliverers();
            Deliverer TempDeliverer = new Deliverer();

            foreach (var deliverer in AllDeliverers)
            {
                if (deliverer.full_name == TEMP_DELIVERER_NAME)
                {
                    TempDeliverer= deliverer;
                    break;
                }
            }

            //if(DelivererInCity.Count > 0)
            //{
            return TempDeliverer;
            //}
        }
    }
}
