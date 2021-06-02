using System.Collections.Generic;
using StoreModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace StoreDL
{
    public class OrderDB
    {
        private StoreDBContext _context;
        private CustomerDB customerDB;

        private LocationDB locationDB;
        public OrderDB(StoreDBContext context)
        {
            _context = context;
            customerDB = new CustomerDB(_context);
            locationDB = new LocationDB(_context);
            
        }

        
        public List<Order> GetAllOrder(){
            
            return _context.Orders
            .Select( order => new Order(order.OrderId, order.UserName, order.LocationId, order.Orderdate, order.Total))
                .ToList();
        }

        public Order GetOrder(int id)
        {
            return _context.Orders.FirstOrDefault(order => order.OrderId == id);
        }

        public Order AddOrder(Order newOrder){
            _context.Orders.Add(newOrder);
            _context.SaveChanges();
            
            return newOrder;
        }

        public List<Item> GetItems(int orderID){
            List<Item> items = _context.Items.Where(
                item => item.OrderId == orderID)
                .Select(
                    item => new Item(item.ItemId, orderID, item.ProductId, item.Quantity)
                ).ToList();

            return items;
        }

        public Item GetItem(int id)
        {
            return _context.Items.FirstOrDefault(item => item.ItemId == id);
        }

        public Customer GetCustomer(string username){
            Customer found = customerDB.GetCustomer(username);

            return new Customer(found.Name, found.UserName);
        }

        public string GetCustomerName(string username){
            Customer found = customerDB.GetCustomer(username);
            return found.Name;
        }

        public Location GetLocation(int locationID){
            Location found =  _context.Locations.FirstOrDefault(location => location.LocationID == locationID);
            if (found == null) return null;
            return new Location(found.LocationName, found.Address, found.LocationID);
        }

        public void DeleteOrder(int id)
        {
            List<Item> items = _context.Items.Where(item => item.OrderId == id).Select(item => item).ToList();

            foreach(Item item in items)
            {
                _context.Items.Remove(item);
                _context.SaveChanges();
            }
            Order toBeDeleted = _context.Orders.FirstOrDefault(order => order.OrderId == id);
            _context.Orders.Remove(toBeDeleted);
            _context.SaveChanges();
        }

        public Item DeleteItem(int id)
        {
            Item toBeDeleted = _context.Items.FirstOrDefault(item => item.ItemId == id);
            _context.Items.Remove(toBeDeleted);
            _context.SaveChanges();
            return toBeDeleted;
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public void AddItem(Item item)
        {
            _context.Items.Add(item);
            _context.SaveChanges();
        }

        public List<Order> GetCustomerOrderByDate(Customer customer){
            List<Order> allOrders = GetAllOrder();
            List<Order> customerOrder = new List<Order>();

            foreach(Order order in allOrders){
                if (order.UserName == customer.UserName){
                    customerOrder.Add(order);
                }
            }

            customerOrder.Sort(delegate(Order x, Order y)
                {
                    return x.Orderdate.CompareTo(y.Orderdate);
                });

            return customerOrder;
        }

        public List<Order> GetCustomerOrderByDateDes(Customer customer)
        {
            List<Order> allOrders = GetAllOrder();
            List<Order> customerOrder = new List<Order>();

            foreach (Order order in allOrders)
            {
                if (order.UserName == customer.UserName)
                {
                    customerOrder.Add(order);
                }
            }

            customerOrder.Sort(delegate (Order y, Order x)
            {
                return x.Orderdate.CompareTo(y.Orderdate);
            });

            return customerOrder;
        }

        public List<Order> GetCustomerOrderByTotal(Customer customer){
            List<Order> allOrders = GetAllOrder();
            List<Order> customerOrder = new List<Order>();

            foreach(Order order in allOrders){
                if (order.UserName == customer.UserName){
                    customerOrder.Add(order);
                }
            }

            customerOrder.Sort(delegate(Order x, Order y)
                {
                    return x.Total.CompareTo(y.Total);
                });

            return customerOrder;
        }

        public List<Order> GetCustomerOrderByTotalDes(Customer customer)
        {
            List<Order> allOrders = GetAllOrder();
            List<Order> customerOrder = new List<Order>();

            foreach (Order order in allOrders)
            {
                if (order.UserName == customer.UserName)
                {
                    customerOrder.Add(order);
                }
            }

            customerOrder.Sort(delegate (Order y, Order x)
            {
                return x.Total.CompareTo(y.Total);
            });

            return customerOrder;
        }

        public List<Order> LocationOrdersBydate(int location){

            List<Order> orders = _context.Orders.Where(ord => ord.LocationId == location).Select(order => order).ToList();

            orders.Sort(delegate(Order x, Order y)
                {
                    return x.Orderdate.CompareTo(y.Orderdate);
                });

            return orders;
        }

        public List<Order> LocationOrdersBydateDes(int location)
        {

            List<Order> orders = _context.Orders.Where(ord => ord.LocationId == location).Select(order => order).ToList();

            orders.Sort(delegate (Order y, Order x)
            {
                return x.Orderdate.CompareTo(y.Orderdate);
            });

            return orders;
        }

        public List<Order> LocationOrdersByTotal(int location){

            List<Order> orders = _context.Orders.Where(ord => ord.LocationId == location).Select(order => order).ToList();

            orders.Sort(delegate(Order x, Order y)
                {
                    return x.Total.CompareTo(y.Total);
                });

            return orders;
        }

        public List<Order> LocationOrdersByTotalDes(int location)
        {

            List<Order> orders = _context.Orders.Where(ord => ord.LocationId == location).Select(order => order).ToList();

            orders.Sort(delegate (Order y, Order x)
            {
                return x.Total.CompareTo(y.Total);
            });

            return orders;
        }
    }
}