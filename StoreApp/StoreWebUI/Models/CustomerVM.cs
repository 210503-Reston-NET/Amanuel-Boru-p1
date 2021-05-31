using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreModels;
namespace StoreWebUI.Models
{
    public class CustomerVM
    {
        public CustomerVM()
        {
        }

        public CustomerVM(Customer customer)
        {
            username = customer.UserName;
            name = customer.Name;
        }
        public string username { get; set; }
        public string name { get; set; }
    }
}
