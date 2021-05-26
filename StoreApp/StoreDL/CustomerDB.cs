using System.Collections.Generic;
using StoreModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace StoreDL
{
    public class CustomerDB
    {
        private StoreDBContext _context;
        public CustomerDB(StoreDBContext context)
        {
            _context = context;
        }

        public Customer AddCustomer(Customer newCustomer){
            _context.Customers.Add(newCustomer);

            _context.SaveChanges();
            return newCustomer;
        }
        public List<Customer> GetAllCustomers(){
            List<Customer> customers = _context.Customers
                .Select( customer => new Customer(customer.Name, customer.UserName, customer.Password))
                .ToList();
            
            return customers;
        }

        public Customer GetCustomer(string username){
            Customer found = _context.Customers.FirstOrDefault(custom =>  custom.UserName == username);

            if (found == null) return null;
            else{
                return new Customer(found.Name, found.UserName, found.Password);
            }
        }
    }
}