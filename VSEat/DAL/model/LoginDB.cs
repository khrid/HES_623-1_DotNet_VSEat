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

        public bool isUserValid(Login login)
        {
            // add sql to get user data from db

            //string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {

                    string query = "SELECT count(*) AS 'result' FROM customers WHERE username=@username AND password=@password";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@username", login.username);
                    cmd.Parameters.AddWithValue("@password", login.password);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        dr.Read();
                        if ((int)dr["result"] == 1)
                        {
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
