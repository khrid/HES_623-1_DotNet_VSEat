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
        List<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
    }
}
