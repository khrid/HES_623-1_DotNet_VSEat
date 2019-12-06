using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class RestaurantsDB : IRestaurantsDB
    {
        public IConfiguration configuration { get; }

        private string connectionString = "";
        public RestaurantsDB(IConfiguration configuration)
        {

            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public List<Restaurant> GetAllRestaurants()
        {
            List<Restaurant> results = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from restaurants order by merchant_name";
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
                            CitiesDB citiesDB = new CitiesDB(configuration);
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
                            CitiesDB citiesDB = new CitiesDB(configuration);
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
