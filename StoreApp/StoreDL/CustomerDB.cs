using System.Collections.Generic;
using StoreModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

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
                .Select( customer => new Customer(customer.Name, customer.UserName))
                .ToList();
            
            return customers;
        }

        public Customer GetCustomer(string username){
            Customer found = _context.Customers.FirstOrDefault(custom =>  custom.UserName == username);

            if (found == null) return null;
            else{
                return new Customer(found.Name, found.UserName);
            }
        }

        public Customer DeleteCustomer(Customer customer)
        {
            Customer toBeDeleted = _context.Customers.FirstOrDefault(cust => cust.UserName == customer.UserName);
            _context.Customers.Remove(toBeDeleted);
            _context.SaveChanges();
            return toBeDeleted;
        }
    }
}