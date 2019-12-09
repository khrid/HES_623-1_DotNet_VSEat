using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class DeliverersDB : IDeliverersDB
    {

        private string connectionString = "";
        private IConfiguration configuration = null;

        public DeliverersDB(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("DefaultConnection");

        }


        public int UpdateDeliverer(Deliverer deliverer)
        {
            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "update deliverers set password=@password, full_name=@full_name " +
                        "where id=@id;";// +
                                                         //"select scope_identity();";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", deliverer.id);
                    cmd.Parameters.AddWithValue("@password", deliverer.password);
                    cmd.Parameters.AddWithValue("@full_name", deliverer.full_name);

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

        public List<Deliverer> GetAllDeliverers()
        {
            List<Deliverer> results = null;
            //string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from deliverers";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Deliverer>();

                            Deliverer member = new Deliverer();

                            member.id = (int)dr["id"];
                            member.username = (string)dr["username"];
                            member.password = (string)dr["password"];
                            member.full_name = (string)dr["full_name"];
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

        public Deliverer GetDelivererById(int id)
        {
            Deliverer result = null;
            //string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from deliverers where id=@id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            if (result == null)
                                result = new Deliverer();

                            result.id = (int)dr["id"];
                            result.username = (string)dr["username"];
                            result.password = (string)dr["password"];
                            result.full_name = (string)dr["full_name"];
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
