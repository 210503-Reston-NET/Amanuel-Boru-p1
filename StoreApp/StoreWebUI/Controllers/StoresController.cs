using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreBL;
using StoreModels;
using StoreWebUI.Models;

namespace StoreWebUI.Controllers
{
    public class StoresController : Controller
    {
        private LocationBL _locationBL;

        public StoresController(LocationBL locationBL)
        {
            _locationBL = locationBL;
        }

        // GET: StoresController
        public ActionResult Index()
        {
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
