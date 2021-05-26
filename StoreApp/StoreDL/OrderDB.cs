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

        public Order AddOrder(Order newOrder){
            _context.Orders.Add(newOrder);
            _context.SaveChanges();
            
            List<Item> items = newOrder.Items;

            foreach(Item item in items){
                _context.Items.Add(item);
                locationDB.changeInventory(newOrder.LocationId, new Inventory(newOrder.LocationId, item.ProductId), -1*item.Quantity);
            }
            _context.SaveChanges();
            return newOrder;
        }

        public List<Item> GetItems(int orderID){
            List<Item> items = _context.Items.Where(
                item => item.OrderId == orderID)
                .Select(
                    item => new Item(orderID, item.ProductId, item.Quantity)
                ).ToList();

            return items;
        }

        public Customer GetCustomer(string username){
            Customer found = customerDB.GetCustomer(username);

            return new Customer(found.Name, found.UserName, found.Password);
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

        public List<Order> LocationOrdersBydate(Location location){
            List<Order> allOrders = GetAllOrder();
            List<Order> locationOrder = new List<Order>();

            foreach(Order order in allOrders){
                if (order.LocationId == location.LocationID){
                    locationOrder.Add(order);
                }
            }

            locationOrder.Sort(delegate(Order x, Order y)
                {
                    return x.Orderdate.CompareTo(y.Orderdate);
                });

            return locationOrder;
        }

        public List<Order> LocationOrdersByTotal(Location location){
            List<Order> allOrders = GetAllOrder();
            List<Order> locationOrder = new List<Order>();

            foreach(Order order in allOrders){
                if (order.LocationId == location.LocationID){
                    locationOrder.Add(order);
                }
            }

            locationOrder.Sort(delegate(Order x, Order y)
                {
                    return x.Total.CompareTo(y.Total);
                });

            return locationOrder;
        }
    }
}