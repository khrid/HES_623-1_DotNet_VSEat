using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class CustomersDB : ICustomersDB
    {

        private string connectionString = "";
        private IConfiguration configuration = null;

        public CustomersDB(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("DefaultConnection");

        }
        public Customer AddCustomer(Customer customer)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "insert into customers(username, password, full_name, fk_cities, address) " +
                        "values(@username, @password, @full_name, @fk_cities, @address);" +
                        "select scope_identity();";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@username", customer.username);
                    cmd.Parameters.AddWithValue("@password", customer.password);
                    cmd.Parameters.AddWithValue("@full_name", customer.full_name);
                    cmd.Parameters.AddWithValue("@fk_cities", customer.city.id);
                    cmd.Parameters.AddWithValue("@address", customer.address);

                    cn.Open();

                    customer.id = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return customer;
        }

        public int UpdateCustomer(Customer customer)
        {
            int result = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "update customers set password=@password, full_name=@full_name, " +
                        "address=@address, fk_cities=@city where id=@id;";// +
                        //"select scope_identity();";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@id", customer.id);
                    cmd.Parameters.AddWithValue("@password", customer.password);
                    cmd.Parameters.AddWithValue("@full_name", customer.full_name);
                    cmd.Parameters.AddWithValue("@address", customer.address);
                    cmd.Parameters.AddWithValue("@city", customer.city.id);

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

        public List<Customer> GetAllCustomers()
        {
            List<Customer> results = null;
            //string connectionString = Configuration.GetConnectionString("DefaultConnection");

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
                            if(dr["address"] != null)
                            {
                                member.address = (string)dr["address"];
                            }
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

        public Customer GetCustomerById(int id)
        {
            Customer result = null;
            //string connectionString = Configuration.GetConnectionString("DefaultConnection");

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
                            if (dr["address"] != null)
                            {
                                result.address = (string)dr["address"];
                            }
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

        public Customer CheckAuthentication(string user, string password)
        {
            Customer result = null;
            //string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from customers where username=@user and password=@password";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@user", user);
                    cmd.Parameters.AddWithValue("@password", password);

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
                            if (dr["address"] != null)
                            {
                                result.address = (string)dr["address"];
                            }
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
