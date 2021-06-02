using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreBL;
using StoreModels;
using StoreWebUI.Models;
using Serilog;

namespace StoreWebUI.Controllers
{
    public class StoresController : Controller
    {
        private ILocationBL _locationBL;
        private IOrderBL _orderBL;
        public StoresController(ILocationBL locationBL, IOrderBL orderBL)
        {
            _locationBL = locationBL;
            _orderBL = orderBL;
        }

        // GET: StoresController
        public ActionResult Index()
        {
            Log.Information("Store Page opened");
            return View(_locationBL.GetAllLocations().Select(location => new LocationVM(location)).ToList());
        }


        // GET: StoresController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StoresController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LocationVM locationVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Log.Information("New storage added");
                    _locationBL.AddLocation(new Location(locationVM.LocationName, locationVM.Address));
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.id = id;
            return View();
        }

        public ActionResult OrderBydate(int id)
        {
            Log.Information("Order By date called");
            ViewBag.id = id;
            List<OrderVM> orders = _orderBL.LocationOrdersBydate(id).Select(order => new OrderVM(order)).ToList();
            return View(orders);
        }

        public ActionResult OrderBydateDes(int id)
        {
            Log.Information("Order By date called");
            ViewBag.id = id;
            List<OrderVM> orders = _orderBL.LocationOrdersBydateDes(id).Select(order => new OrderVM(order)).ToList();
            return View(orders);
        }

        public ActionResult OrderBYTotal(int id)
        {
            Log.Information("Order by total called");
            ViewBag.id = id;
            List<OrderVM> orders = _orderBL.LocationOrdersByTotal(id).Select(order => new OrderVM(order)).ToList();
            return View(orders);
        }

        public ActionResult OrderBYTotalDes(int id)
        {
            Log.Information("Order by total called");
            ViewBag.id = id;
            List<OrderVM> orders = _orderBL.LocationOrdersByTotalDes(id).Select(order => new OrderVM(order)).ToList();
            return View(orders);
        }

        // GET: StoresController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(new LocationVM(_locationBL.GetLocation(id)));
        }

        // POST: StoresController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LocationVM locationVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Log.Information("Location Edited");
                    _locationBL.UpdateLocation(new Location(locationVM.LocationName, locationVM.Address, id));
                    return RedirectToAction(nameof(Index));
                }
                return View(); 
            }
            catch
            {
                return View();
            }
        }

        // GET: StoresController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(new LocationVM(_locationBL.GetLocation(id)));
        }

        // POST: StoresController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Log.Information("Location deleted");
                    _locationBL.DeleteLocation(_locationBL.GetLocation(id));
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
