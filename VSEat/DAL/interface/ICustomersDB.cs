using DTO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DAL
{
    public interface ICustomersDB
    {
        IConfiguration Configuration { get; }
        List<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        Customer CheckAuthentication(string user, string password);
    }
}