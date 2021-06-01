using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreModels;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        public string username { get; set; }

        [Required]
        public string name { get; set; }
    }
}
