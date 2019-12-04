using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    interface ICustomersManager
{
        ICustomersDB CustomersDB { get; }
        Customer AddCustomer(Customer customer);
        List<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        Customer CheckAuthentication(string user, string password);
    }
}
