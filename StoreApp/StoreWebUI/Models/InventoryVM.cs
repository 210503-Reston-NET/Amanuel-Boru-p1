using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreModels;

namespace StoreWebUI.Models
{
    public class InventoryVM
    {
        public InventoryVM()
        {
        }

        public InventoryVM(Inventory inventory)
        {
            InventoryId = inventory.InventoryId;
            LocationId = inventory.LocationId;
            ProductId = inventory.ProductId.ToString();
            Quantity = inventory.Quantity;
        }

        public InventoryVM(Inventory inventory, string name)
        {
            InventoryId = inventory.InventoryId;
            LocationId = inventory.LocationId;
            ProductId = inventory.ProductId.ToString();
            ProductName = name;
            Quantity = inventory.Quantity;
        }

        public InventoryVM(int locationId, List<SelectListItem> products)
        {
            LocationId = locationId;
            ProductList = products;
        }

        public int InventoryId { get; set; }
        public int LocationId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }

        public List<SelectListItem> ProductList { get; set; }
    }
}
