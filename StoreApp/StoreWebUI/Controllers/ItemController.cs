using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreBL;
using StoreModels;
using StoreWebUI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serilog;

namespace StoreWebUI.Controllers
{
    public class ItemController : Controller
    {
        private IProductBL _productBL;
        private IOrderBL _orderBL;
        private ILocationBL _locationBL;

        public ItemController(IProductBL productBL, IOrderBL orderBL, ILocationBL locationBL)
        {
            _productBL = productBL;
            _orderBL = orderBL;
            _locationBL = locationBL;
        }


        // GET: ItemController
        public ActionResult Index(int id)
        {
            Log.Information("Item page opened");
            ViewBag.OrderID = id;
            List<Item> items = _orderBL.GetItems(id);
            List<ItemVM> itemVMs = new List<ItemVM>();
            foreach(Item item in items)
            {
                itemVMs.Add(new ItemVM(item, _productBL.GetProduct(item.ProductId).ProductName));
            }
            return View(itemVMs);
        }


        // GET: ItemController/Create
        public ActionResult Create(int id)
        {
            int locationID = _orderBL.GetOrder(id).LocationId;
            List<Inventory> inventories = _locationBL.GetInventory(locationID);
            List<SelectListItem> productList = new List<SelectListItem>();
            string text;
            foreach (Inventory inventory in inventories)
            {
                text = "Product: " + _productBL.GetProduct(inventory.ProductId).ProductName + "  --  Available: " + inventory.Quantity.ToString();
                productList.Add(new SelectListItem { Text = text , Value = inventory.ProductId.ToString() });
            }

            ItemVM item = new ItemVM { OrderId = id, ProductList = productList };

            return View(item);
        }

        // POST: ItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemVM itemVM)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    int locationID = _orderBL.GetOrder(itemVM.OrderId).LocationId;
                    Inventory inventory = _locationBL.GetInventoryItemFromLocation(locationID, Int32.Parse(itemVM.ProductId));
                    if (inventory.Quantity < itemVM.Quantity)
                    {
                        return RedirectToAction(nameof(Create), new { id = itemVM.OrderId });
                    }
                    _orderBL.AddItem(new Item(itemVM.OrderId, Int32.Parse(itemVM.ProductId), itemVM.Quantity));
                    
                    inventory.Quantity -= itemVM.Quantity;
                    _locationBL.UpdateInventory(inventory);
                    
                    double price = _productBL.GetProduct(Int32.Parse(itemVM.ProductId)).Price;
                    double total = price * itemVM.Quantity;

                    Order order = _orderBL.GetOrder(itemVM.OrderId);
                    order.Total += total;
                    _orderBL.UpdateOrder(order);
                    Log.Information("Item Created");
                    return RedirectToAction(nameof(Index), new { id = itemVM.OrderId });
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: ItemController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                Log.Information("Item eddited");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ItemController/Delete/5
        public ActionResult Delete(int id)
        {
            Item item = _orderBL.GetItem(id);
            return View(new ItemVM(item, _productBL.GetProduct(item.ProductId).ToString()));
        }

        // POST: ItemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Log.Information("Item deleted");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
