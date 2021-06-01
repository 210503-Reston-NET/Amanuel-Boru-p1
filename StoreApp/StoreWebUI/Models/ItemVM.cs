using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreModels;

namespace StoreWebUI.Models
{
    public class ItemVM
    {
        public ItemVM()
        {
        }

        public ItemVM(Item item, string productName)
        {
            ItemId = item.ItemId;
            OrderId = item.OrderId;
            ProductId = item.ProductId.ToString();
            ProductName = productName;
            Quantity = item.Quantity;
        }

        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }

        public List<SelectListItem> ProductList { get; set; }
    }
}
