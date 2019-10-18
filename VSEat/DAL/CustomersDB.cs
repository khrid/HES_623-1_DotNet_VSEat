using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class CustomersDB
    {
        public IConfiguration Configuration;

        public CustomersDB(IConfiguration configuration)
        {

            Configuration = configuration;

        }


        public List<Customer> GetAllCustomers()
        {
            List<Customer> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from customers";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Customer>();

                            Customer member = new Customer();

                            member.id = (int)dr["id"];
                            member.username = (string)dr["username"];
                            member.password = (string)dr["password"];
                            member.full_name = (string)dr["full_name"];
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

        public Customer GetCustomerById(int id)
        {
            Customer result = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from customers where id=@id";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", id);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            if (result == null)
                                result = new Customer();

                            result.id = (int)dr["id"];
                            result.username = (string)dr["username"];
                            result.password = (string)dr["password"];
                            result.full_name = (string)dr["full_name"];
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
