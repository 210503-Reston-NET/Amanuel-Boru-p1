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
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username must be made up of numbers, letters and \'_\'")]
        public string username { get; set; }

        [Required]
        public string name { get; set; }
    }
}
