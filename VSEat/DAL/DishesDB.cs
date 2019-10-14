using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class DishesDB
    {
        public IConfiguration Configuration;

        public DishesDB(IConfiguration configuration)
        {

            Configuration = configuration;

        }


        public List<Dish> GetAllDishes()
        {
            List<Dish> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from dishes";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Dish>();

                            Dish member = new Dish();

                            member.id = (int)dr["id"];
                            member.name = (string)dr["name"];
                            member.price = (int)dr["price"];
                            member.status = (bool)dr["status"];
                            // Voir si modifications souhaitées
                            RestaurantsDB restaurantsDB = new RestaurantsDB(Configuration);
                            member.restaurant = restaurantsDB.GetRestaurantById((int)dr["fk_restaurants"]);

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

        public Dish GetDishById(int id)
        {
            Dish result = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = $"Select * from dishes where id={id}";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        dr.Read();
                        if (result == null)
                            result = new Dish();

                        result.id = (int)dr["id"];
                        result.name = (string)dr["name"];
                        result.price = (int)dr["price"];
                        result.status = (bool)dr["status"];
                        // Voir si modifications souhaitées
                        RestaurantsDB restaurantsDB = new RestaurantsDB(Configuration);
                        result.restaurant = restaurantsDB.GetRestaurantById((int)dr["fk_restaurants"]);
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
