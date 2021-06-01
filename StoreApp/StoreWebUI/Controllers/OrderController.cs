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
    public class OrderController : Controller
    {
        private IOrderBL _orderBL;
        private ICustomerBL _customerBL;
        private ILocationBL _locationBL;

        public OrderController(IOrderBL orderBL, ICustomerBL customerBL, ILocationBL locationBL)
        {
            _orderBL = orderBL;
            _customerBL = customerBL;
            _locationBL = locationBL;
        }
        // GET: OrderController
        public ActionResult Index()
        {
            Log.Information("Order page opened");
            return View(_orderBL.GetAllOrder().Select(ord => new OrderVM(ord)).ToList());
        }


        // GET: OrderController/Create
        public ActionResult Create(string id)
        {
            ViewData["message"] = id;

            List<Location> locations = _locationBL.GetAllLocations();
            List<SelectListItem> locationList = new List<SelectListItem>();
            foreach (Location location in locations)
            {
                locationList.Add(new SelectListItem { Text = location.LocationName, Value = location.LocationID.ToString() });
            }

            OrderVM order = new OrderVM { LocationList = locationList, Total = 0, Orderdate = DateTime.Now };
            return View(order);
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderVM orderVM)
        {
            try
            {
                if (_customerBL.UserNameExists(orderVM.UserName) == false)
                {
                    return RedirectToAction(nameof(Create), new { id = "Username doesnt exist" });
                }
                Log.Information("Order created");
                _orderBL.AddOreder(new Order(orderVM.UserName, Int32.Parse(orderVM.LocationId), orderVM.Orderdate, orderVM.Total));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            
            return View(new OrderVM(_orderBL.GetOrder(id)));
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Log.Information("Order deleted");
                    _orderBL.DeleteOrder(id);
                    return RedirectToAction(nameof(Index));
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
