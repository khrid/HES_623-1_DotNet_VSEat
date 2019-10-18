using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class OrdersStatusHistoryDB
    {
        public IConfiguration Configuration;

        public OrdersStatusHistoryDB(IConfiguration configuration)
        {

            Configuration = configuration;

        }


        public List<OrdersStatusHistory> GetAllOrdersStatusHistory()
        {
            List<OrdersStatusHistory> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

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
                            OrdersDB ordersDB = new OrdersDB(Configuration);
                            member.order = ordersDB.GetOrderById((int)dr["fk_orders"]);
                            OrdersStatusDB ordersStatusDB = new OrdersStatusDB(Configuration);
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
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

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
                            OrdersDB ordersDB = new OrdersDB(Configuration);
                            result.order = ordersDB.GetOrderById((int)dr["fk_orders"]);
                            OrdersStatusDB ordersStatusDB = new OrdersStatusDB(Configuration);
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
