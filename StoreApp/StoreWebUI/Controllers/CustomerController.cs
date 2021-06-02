using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreModels;
using StoreBL;
using StoreWebUI.Models;
using Serilog;

namespace StoreWebUI.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerBL _customerBL;
        private IOrderBL _orderBL;

        public CustomerController(ICustomerBL customerBL, IOrderBL orderBL)
        {
            _customerBL = customerBL;
            _orderBL = orderBL;
        }


        // GET: CustomerController
        public ActionResult Index()
        {
            Log.Information("Customer page opened");
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
            Log.Information("Order by date from customer");
            ViewBag.UserName = username;
            List<OrderVM> orders = _orderBL.CustomerOrdersBydate(new Customer(username)).Select(order => new OrderVM(order)).ToList();
            return View(orders);
        }

        public ActionResult OrderBydateDes(string username)
        {
            Log.Information("Order by date from customer");
            ViewBag.UserName = username;
            List<OrderVM> orders = _orderBL.CustomerOrdersBydateDes(new Customer(username)).Select(order => new OrderVM(order)).ToList();
            return View(orders);
        }

        public ActionResult OrderBYTotal(string username)
        {
            Log.Information("Order by total from customer");
            ViewBag.UserName = username;
            List<OrderVM> orders = _orderBL.CustomerOrdersByTotal(new Customer(username)).Select(order => new OrderVM(order)).ToList();
            return View(orders);
        }

        public ActionResult OrderBYTotalDes(string username)
        {
            Log.Information("Order by total from customer");
            ViewBag.UserName = username;
            List<OrderVM> orders = _orderBL.CustomerOrdersByTotalDes(new Customer(username)).Select(order => new OrderVM(order)).ToList();
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
                    Log.Information("Customer created");
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
                    Log.Information("Customer Deleted");
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
