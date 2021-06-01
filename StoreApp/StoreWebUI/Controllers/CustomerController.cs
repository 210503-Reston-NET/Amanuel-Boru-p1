using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreModels;
using StoreBL;
using StoreWebUI.Models;

namespace StoreWebUI.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerBL _customerBL;
        private OrderBL _orderBL;

        public CustomerController(CustomerBL customerBL, OrderBL orderBL)
        {
            _customerBL = customerBL;
            _orderBL = orderBL;
        }

        // GET: CustomerController
        public ActionResult Index()
        {
            return View(_customerBL.GetAllCustomers().Select(cust => new CustomerVM(cust)).ToList());
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(string username)
        {
            ViewBag.UserName = username;
            return View();
        }

        public ActionResult OrderBydate(string username)
        {
            ViewBag.UserName = username;
            List<OrderVM> orders = _orderBL.CustomerOrdersBydate(new Customer(username)).Select(order => new OrderVM(order)).ToList();
            return View(orders);
        }

        public ActionResult OrderBYTotal(string username)
        {
            ViewBag.UserName = username;
            List<OrderVM> orders = _orderBL.CustomerOrdersByTotal(new Customer(username)).Select(order => new OrderVM(order)).ToList();
            return View(orders);
        }

        // GET: CustomerController/Create
        public ActionResult Create(string id)
        {
            ViewData["message"] = id;
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerVM customerVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_customerBL.UserNameExists(customerVM.username))
                    {
                        return RedirectToAction(nameof(Create), new { id = "Username is already taken Please try again"});
                    }

                    _customerBL.AddCustomer(new Customer(customerVM.name, customerVM.username));
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }


        // GET: CustomerController/Delete/5
        public ActionResult Delete(string username)
        {
            ViewData["username"] = username;
            return View(new CustomerVM(_customerBL.GetCustomer(username)));
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, CustomerVM collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _customerBL.DeleteCustomer(new Customer(id));
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
