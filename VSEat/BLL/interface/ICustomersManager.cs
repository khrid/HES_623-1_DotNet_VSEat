using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public interface ICustomersManager
    {
        Customer AddCustomer(Customer customer);
        int UpdateCustomer(Customer customer);
        List<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        Customer CheckAuthentication(string user, string password);
    }
}
