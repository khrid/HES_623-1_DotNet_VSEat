using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using DTO;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class OrdersStatusManager : IOrdersStatusManager
    {
        private IOrdersStatusDB OrdersStatusDB { get; }

        public const string COMMANDE_RECUE = "Commande reçue";
        public const string COMMANDE_PREPA = "Commande en cours de préparation";
        public const string COMMANDE_LIVRAISON = "Commande en cours de livraison";
        public const string COMMANDE_LIVREE = "Commande livrée";
        public const string COMMANDE_ANNULEE = "Commande annulée";

        public OrdersStatusManager(IOrdersStatusDB ordersStatusDB)
        {
            OrdersStatusDB = ordersStatusDB;
        }

        public List<OrdersStatus> GetAllOrdersStatus()
        {
            return OrdersStatusDB.GetAllOrdersStatus();
        }

        public OrdersStatus GetOrdersStatusById(int id)
        {
            return OrdersStatusDB.GetOrdersStatusById(id);
        }

        public OrdersStatus GetOrdersStatusByStatus(string status)
        {
            OrdersStatus ordersStatus = new OrdersStatus();
            switch (status)
            {
                case COMMANDE_RECUE:
                case COMMANDE_PREPA:
                case COMMANDE_LIVRAISON:
                case COMMANDE_LIVREE:
                case COMMANDE_ANNULEE:
                    List<OrdersStatus> AllOrdersStatus = GetAllOrdersStatus();
                    foreach (var item in AllOrdersStatus)
                    {
                        if(item.status == status)
                        {
                            ordersStatus = item;
                        }
                    }
                    break;
            }
            return ordersStatus;
        }
    }
}
