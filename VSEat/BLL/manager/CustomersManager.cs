using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using DTO;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class CustomersManager : ICustomersManager
    {
        public ICustomersDB CustomersDB { get; }

        public CustomersManager(IConfiguration configuration)
        {
            CustomersDB = new CustomersDB(configuration);
        }

        public Customer AddCustomer(Customer customer)
        {
            return CustomersDB.AddCustomer(customer);
        }

        public List<Customer> GetAllCustomers()
        {
            return CustomersDB.GetAllCustomers();
        }

        public Customer GetCustomerById(int id)
        {
            return CustomersDB.GetCustomerById(id);
        }

        public Customer CheckAuthentication(string user, string password)
        {
            return CustomersDB.CheckAuthentication(user, password);
        }
    }
}
