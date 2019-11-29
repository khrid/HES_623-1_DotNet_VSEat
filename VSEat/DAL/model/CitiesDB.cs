using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class CitiesDB : ICitiesDB
    {
        public IConfiguration Configuration { get; }

        private string connectionString = "Data Source=153.109.124.35;Initial Catalog=CrittinMeyer_ValaisEat;Persist Security Info=True;User ID=6231db;Password=Pwd46231.";

        public CitiesDB(IConfiguration configuration)
        {

            Configuration = configuration;

        }


        public List<City> GetAllCities()
        {
            List<City> results = null;
            //string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from cities order by name";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<City>();

                            City member = new City();

                            member.id = (int)dr["id"];
                            member.zip_code = (int)dr["zip_code"];
                            member.name = (string)dr["name"];

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

        public City GetCityById(int id)
        {
            City result = null;
            //string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from cities where id=@id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        if (dr.Read())
                        {

                            if (result == null)
                                result = new City();

                            result.id = (int)dr["id"];
                            result.zip_code = (int)dr["zip_code"];
                            result.name = (string)dr["name"];
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
