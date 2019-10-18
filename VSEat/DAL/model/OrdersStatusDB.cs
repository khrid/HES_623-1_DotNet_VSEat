using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class OrdersStatusDB : IOrdersStatusDB
    {
        public IConfiguration Configuration { get; }

        public OrdersStatusDB(IConfiguration configuration)
        {

            Configuration = configuration;

        }


        public List<OrdersStatus> GetAllOrdersStatus()
        {
            List<OrdersStatus> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from orders_status";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<OrdersStatus>();

                            OrdersStatus member = new OrdersStatus();

                            member.id = (int)dr["id"];
                            member.status = (string)dr["status"];

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

        public OrdersStatus GetOrdersStatusById(int id)
        {
            OrdersStatus result = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from orders_status where id=@id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            if (result == null)
                                result = new OrdersStatus();

                            result.id = (int)dr["id"];
                            result.status = (string)dr["status"];
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
