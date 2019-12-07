using DTO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DAL
{
    public interface ICustomersDB
    {
        Customer AddCustomer(Customer customer);
        int UpdateCustomer(Customer customer);
        List<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        Customer CheckAuthentication(string user, string password);
    }
}