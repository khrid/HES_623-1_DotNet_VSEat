﻿using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class OrdersDB : IOrdersDB
    {
        public IConfiguration configuration { get; }
        private string connectionString = "";

        public OrdersDB(IConfiguration configuration)
        {

            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }



        public List<Order> GetAllOrders()
        {
            List<Order> results = null;
            //string connectionString = Configuration.GetConnectionString("DefaultConnection");
            //string connectionString = "Data Source=153.109.124.35;Initial Catalog=CrittinMeyer_ValaisEat;Persist Security Info=True;User ID=6231db;Password=Pwd46231.";

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from orders";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Order>();

                            Order member = new Order();

                            member.id = (int)dr["id"];
                            member.delivery_time_requested = (DateTime)dr["delivery_time_requested"];
                            // Voir si modifications souhaitées
                            CustomersDB customersDB = new CustomersDB(configuration);
                            member.customer = customersDB.GetCustomerById((int)dr["fk_customers"]);
                            DeliverersDB deliverersDB = new DeliverersDB(configuration);
                            member.deliverer = deliverersDB.GetDelivererById((int)dr["fk_deliverers"]);

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

        public Order GetOrderById(int id)
        {
            Order result = null;
            //string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from orders where id=@id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            if (result == null)
                                result = new Order();

                            result.id = (int)dr["id"];
                            result.delivery_time_requested = (DateTime)dr["delivery_time_requested"];
                            // Voir si modifications souhaitées
                            CustomersDB customersDB = new CustomersDB(configuration);
                            result.customer = customersDB.GetCustomerById((int)dr["fk_customers"]);
                            DeliverersDB deliverersDB = new DeliverersDB(configuration);
                            result.deliverer = deliverersDB.GetDelivererById((int)dr["fk_deliverers"]);
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

        public Order AddOrder(Order order)
        {

            //string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "insert into orders(fk_customers, fk_deliverers, delivery_time_requested) " +
                        "values(@fk_customers, @fk_deliverers, @delivery_time_requested);" +
                        "select scope_identity();";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@fk_customers", order.customer.id);
                    cmd.Parameters.AddWithValue("@fk_deliverers", order.deliverer.id);
                    cmd.Parameters.AddWithValue("@delivery_time_requested", order.delivery_time_requested);

                    cn.Open();

                    order.id = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return order;
        }

        public int UpdateOrder(Order order)
        {
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE orders SET fk_deliverers=@fk_deliverers, delivery_time_requested=@delivery_time_requested WHERE id=@id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", order.id);
                    cmd.Parameters.AddWithValue("@fk_deliverers", order.deliverer.id);
                    cmd.Parameters.AddWithValue("@delivery_time_requested", order.delivery_time_requested);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public int DeleteOrderById(int id)
        {
            int result = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM orders WHERE id=@id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();
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

