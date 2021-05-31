using System;
using StoreDL;
using StoreModels;
using System.Collections.Generic;

namespace StoreBL
{
    public class CustomerBL
    {
        private CustomerDB _customerDB;

        public CustomerBL(CustomerDB customerDB)
        {
            _customerDB = customerDB;
        }
        public Customer AddCustomer(Customer newCustomer){
            if (UserNameExists(newCustomer.UserName)){
                throw new Exception("Username has already been taken");
            }
            return _customerDB.AddCustomer(newCustomer);
        }

        public bool UserNameExists(string userName){
            if (this.GetCustomer(userName) != null) return true;

            return false;
        }

        public List<Customer> GetAllCustomers(){
            return _customerDB.GetAllCustomers();
        }

        public Customer GetCustomer(string username){
            return _customerDB.GetCustomer(username);
        }

        public Customer DeleteCustomer(Customer customer)
        {
            return _customerDB.DeleteCustomer(customer);
        }
    }
}