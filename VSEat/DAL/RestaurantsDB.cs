using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class RestaurantsDB
    {
        public IConfiguration Configuration;

        public RestaurantsDB(IConfiguration configuration)
        {

            Configuration = configuration;

        }


        public List<Restaurant> GetAllRestaurants()
        {
            List<Restaurant> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from restaurants";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Restaurant>();

                            Restaurant member = new Restaurant();

                            member.id = (int)dr["id"];
                            member.merchant_name = (string)dr["merchant_name"];
                            // Voir si modifications souhaitées
                            CitiesDB citiesDB = new CitiesDB(Configuration);
                            member.city = citiesDB.GetCityById((int)dr["fk_cities"]);

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

        public Restaurant GetRestaurantById(int id)
        {
            Restaurant result = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from restaurants where id=@id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            if (result == null)
                                result = new Restaurant();

                            result.id = (int)dr["id"];
                            result.merchant_name = (string)dr["merchant_name"];
                            // Voir si modifications souhaitées
                            CitiesDB citiesDB = new CitiesDB(Configuration);
                            result.city = citiesDB.GetCityById((int)dr["fk_cities"]);
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
