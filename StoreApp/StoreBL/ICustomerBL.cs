using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreDL;
using StoreModels;

namespace StoreBL
{
    public interface ICustomerBL
    {

        Customer AddCustomer(Customer newCustomer);

        bool UserNameExists(string userName);

        List<Customer> GetAllCustomers();

        Customer GetCustomer(string username);

        Customer DeleteCustomer(Customer customer);
    }
 }

