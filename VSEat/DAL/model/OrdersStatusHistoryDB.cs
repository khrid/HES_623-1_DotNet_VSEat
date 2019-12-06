using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class OrdersStatusHistoryDB : IOrdersStatusHistoryDB
    {
        public IConfiguration configuration { get; }
        private string connectionString = "";

        public OrdersStatusHistoryDB(IConfiguration configuration)
        {

            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("DefaultConnection");

        }


        public List<OrdersStatusHistory> GetAllOrdersStatusHistory()
        {
            List<OrdersStatusHistory> results = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from orders_status_history";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<OrdersStatusHistory>();

                            OrdersStatusHistory member = new OrdersStatusHistory();

                            member.id = (int)dr["id"];
                            member.created_at = (DateTime)dr["created_at"];
                            // Voir si modifications souhaitées
                            OrdersDB ordersDB = new OrdersDB(configuration);
                            member.order = ordersDB.GetOrderById((int)dr["fk_orders"]);
                            OrdersStatusDB ordersStatusDB = new OrdersStatusDB(configuration);
                            member.ordersStatus = ordersStatusDB.GetOrdersStatusById((int)dr["fk_orders_status"]);

                            results.Add(member);

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return results;

        }

        public OrdersStatusHistory GetOrdersStatusHistoryById(int id)
        {
            OrdersStatusHistory result = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from orders_status_history where id=@id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            if (result == null)
                                result = new OrdersStatusHistory();

                            result.id = (int)dr["id"];
                            result.created_at = (DateTime)dr["created_at"];
                            // Voir si modifications souhaitées
                            OrdersDB ordersDB = new OrdersDB(configuration);
                            result.order = ordersDB.GetOrderById((int)dr["fk_orders"]);
                            OrdersStatusDB ordersStatusDB = new OrdersStatusDB(configuration);
                            result.ordersStatus = ordersStatusDB.GetOrdersStatusById((int)dr["fk_orders_status"]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;

        }

        public OrdersStatusHistory AddOrdersStatusHistory(OrdersStatusHistory ordersStatusHistory)
        {

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "insert into orders_status_history(fk_orders, fk_orders_status, created_at) " +
                        "values(@fk_orders, @fk_orders_status, @created_at);" +
                        "select scope_identity();";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@fk_orders", ordersStatusHistory.order.id);
                    cmd.Parameters.AddWithValue("@fk_orders_status", ordersStatusHistory.ordersStatus.id);
                    cmd.Parameters.AddWithValue("@created_at", ordersStatusHistory.created_at);

                    cn.Open();

                    ordersStatusHistory.id = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return ordersStatusHistory;
        }

        public OrdersStatusHistory GetCurrentOrderStatusHistoryForOrder(int id)
        {
            OrdersStatusHistory result = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select top 1 * from orders_status_history where fk_orders=@id order by created_at desc;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            if (result == null)
                                result = new OrdersStatusHistory();

                            result.id = (int)dr["id"];
                            result.created_at = (DateTime)dr["created_at"];
                            // Voir si modifications souhaitées
                            OrdersDB ordersDB = new OrdersDB(configuration);
                            result.order = ordersDB.GetOrderById((int)dr["fk_orders"]);
                            OrdersStatusDB ordersStatusDB = new OrdersStatusDB(configuration);
                            result.ordersStatus = ordersStatusDB.GetOrdersStatusById((int)dr["fk_orders_status"]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }
    }
}
