using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using DTO;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class LoginDB : ILoginDB
    {
        private string connectionString = "";

        public LoginDB(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public bool isUserValid(Login login, string type)
        {
            //string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    if(String.IsNullOrEmpty(login.username) || String.IsNullOrEmpty(login.password))
                    {
                        return false;
                    }
                    string table = "customers";
                    if(type == "customer")
                    {
                        table = "customers";
                    } else if (type == "deliverer")
                    {
                        table = "deliverers";
                    }
                    string query = "SELECT id,full_name FROM "+table+" WHERE username=@username AND password=@password";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@username", login.username);
                    cmd.Parameters.AddWithValue("@password", login.password);
                    
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        if (dr.Read())
                        {
                            login.id = (int)dr["id"];
                            login.fullName= (string)dr["full_name"];
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
