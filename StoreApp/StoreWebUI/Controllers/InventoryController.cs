using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreModels;
using StoreBL;
using StoreWebUI.Models;

namespace StoreWebUI.Controllers
{
    public class InventoryController : Controller
    {
        private LocationBL _locationBL;
        private ProductBL _productBL;

        public InventoryController(LocationBL locationBL, ProductBL productBL)
        {
            _locationBL = locationBL;
            _productBL = productBL;
        }

        // GET: InventoryController
        public ActionResult Index(int id)
        {
            ViewBag.Location = _locationBL.GetLocation(id);
            return View(_locationBL.GetInventory(_locationBL.GetLocation(id))
                        .Select(inventory => new InventoryVM {
                            InventoryId = inventory.InventoryId,
                            LocationId = inventory.LocationId,
                            ProductId = inventory.ProductId.ToString(),
                            ProductName = _productBL.GetProduct(inventory.ProductId).ProductName,
                            Quantity = inventory.Quantity,
                        }).ToList());
        }


        // GET: InventoryController/Create
        public ActionResult Create(int id)
        {
            List<Product> products = _productBL.GetAllProducts();
            List<SelectListItem> productList = new List<SelectListItem>();
            foreach(Product product in products)
            {
                productList.Add(new SelectListItem { Text = product.ProductName, Value = product.ProductId.ToString() });
            }

            InventoryVM inventory = new InventoryVM(id, productList);
            
            return View(inventory);
        }

        // POST: InventoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InventoryVM inventoryVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _locationBL.AddItemToLocation(inventoryVM.LocationId, new Inventory(inventoryVM.LocationId, Int32.Parse(inventoryVM.ProductId), inventoryVM.Quantity));
                    return RedirectToAction(nameof(Index), new { id = inventoryVM.LocationId });
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: InventoryController/Edit/5
        public ActionResult Edit(int id)
        {
            Inventory item = _locationBL.GetInventoryItem(id);
            Product product = _productBL.GetProduct(item.ProductId);
            ViewData["ProductName"] = product.ProductName;
            ViewData["LocationId"] = item.LocationId;
            return View(new InventoryVM(item, product.ProductName));
        }

        // POST: InventoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, InventoryVM inventoryVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _locationBL.UpdateInventory(new Inventory(inventoryVM.InventoryId, inventoryVM.LocationId, Int32.Parse(inventoryVM.ProductId), inventoryVM.Quantity));
                    return RedirectToAction(nameof(Index), new { id = inventoryVM.LocationId });
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: InventoryController/Delete/5
        public ActionResult Delete(int id)
        {
            Inventory inventory = _locationBL.GetInventoryItem(id);
            Product product = _productBL.GetProduct(inventory.ProductId);
            ViewData["LocationId"] = inventory.LocationId;
            return View(new InventoryVM(inventory, product.ProductName));
        }

        // POST: InventoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, InventoryVM collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int locationId = _locationBL.DeleteItem(id).LocationId;
                    return RedirectToAction(nameof(Index), new { id = locationId });
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
