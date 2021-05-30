using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StoreWebUI.Models
{
    public class ProductVM
    {
        public ProductVM()
        {
        }

        public ProductVM(Product product)
        {
            ProductId = product.ProductId;
            ProductName = product.ProductName;
            Price = product.Price;
        }

        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        [RegularExpression(@"^(0|-?\d{0,16}(\.\d{0,2})?)$")]
        public double Price { get; set; }
    }
}
