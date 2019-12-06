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
        private string connectionString = "Data Source=153.109.124.35;Initial Catalog=CrittinMeyer_ValaisEat;Persist Security Info=True;User ID=6231db;Password=Pwd46231.";
        public IConfiguration Configuration { get; }

        public LoginDB(IConfiguration configuration)
        {
            Configuration = configuration;
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
