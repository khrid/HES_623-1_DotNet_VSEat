using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class OrderDishesDB
    {
        public IConfiguration Configuration;

        public OrderDishesDB(IConfiguration configuration)
        {

            Configuration = configuration;

        }


        public List<OrderDish> GetAllOrderDishes()
        {
            List<OrderDish> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from order_dishes";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<OrderDish>();

                            OrderDish member = new OrderDish();

                            member.id = (int)dr["id"];
                            member.quantity = (int)dr["quantity"];
                            // Voir si modifications souhaitées
                            OrdersDB ordersDB = new OrdersDB(Configuration);
                            member.order = ordersDB.GetOrderById((int)dr["fk_orders"]);
                            DishesDB dishesDB = new DishesDB(Configuration);
                            member.dish = dishesDB.GetDishById((int)dr["fk_dishes"]);

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

        public OrderDish GetOrderById(int id)
        {
            OrderDish result = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from order_dishes where id=@id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            if (result == null)
                                result = new OrderDish();

                            result.id = (int)dr["id"];
                            result.quantity = (int)dr["quantity"];
                            // Voir si modifications souhaitées
                            OrdersDB ordersDB = new OrdersDB(Configuration);
                            result.order = ordersDB.GetOrderById((int)dr["fk_orders"]);
                            DishesDB dishesDB = new DishesDB(Configuration);
                            result.dish = dishesDB.GetDishById((int)dr["fk_dishes"]);
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
