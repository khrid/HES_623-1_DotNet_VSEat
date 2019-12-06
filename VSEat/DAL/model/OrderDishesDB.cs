using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class OrderDishesDB : IOrderDishesDB
    {
        public IConfiguration Configuration { get; }

        private string connectionString = "";
        public OrderDishesDB(IConfiguration configuration)
        {

            Configuration = configuration;
            connectionString = configuration.GetConnectionString("DefaultConnection");

        }


        public List<OrderDish> GetAllOrderDishes()
        {
            List<OrderDish> results = null;
            //string connectionString = Configuration.GetConnectionString("DefaultConnection");

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

        public OrderDish GetOrderDishById(int id)
        {
            OrderDish result = null;
            //string connectionString = Configuration.GetConnectionString("DefaultConnection");

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

        public OrderDish AddOrderDish(OrderDish orderDish)
        {

            //string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "insert into order_dishes(fk_orders, fk_dishes, quantity) " +
                        "values(@fk_orders, @fk_dishes, @quantity);" +
                        "select scope_identity();";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@fk_orders", orderDish.order.id);
                    cmd.Parameters.AddWithValue("@fk_dishes", orderDish.dish.id);
                    cmd.Parameters.AddWithValue("@quantity", orderDish.quantity);

                    cn.Open();

                    orderDish.id = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return orderDish;
        }

        public List<OrderDish> GetOrderDishByOrderId(int id)
        {
            List<OrderDish> results = null;
            //string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from order_dishes where fk_orders = @id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<OrderDish>();

                            OrderDish orderDish = new OrderDish();

                            orderDish.id = (int)dr["id"];
                            orderDish.quantity = (int)dr["quantity"];
                            // Voir si modifications souhaitées
                            OrdersDB ordersDB = new OrdersDB(Configuration);
                            orderDish.order = ordersDB.GetOrderById((int)dr["fk_orders"]);
                            DishesDB dishesDB = new DishesDB(Configuration);
                            orderDish.dish = dishesDB.GetDishById((int)dr["fk_dishes"]);

                            results.Add(orderDish);

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

    }
}
