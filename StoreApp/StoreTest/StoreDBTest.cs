using StoreModels;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using StoreDL;
using System.Linq;
using System;

namespace StoreTest
{
    public class StoreDBTest
    {
        
        private readonly DbContextOptions<StoreDBContext> options;
        private bool initialized = false;
        public StoreDBTest()
        {
            options = new DbContextOptionsBuilder<StoreDBContext>().UseSqlite("Filename=Test.db").Options;
            seed();
        }
        
        [Fact]
        public void GetAllCustomersShouldReturnAllCustomers(){
            using (var context = new StoreDBContext(options)){
                CustomerDB _customerDB = new CustomerDB(context);
                var customers = _customerDB.GetAllCustomers();
                int count = customers.Count;

                Assert.Equal(1, count);
            }
        }
        
        [Fact]
        public void GetCustomerReturnsCustomer(){
            using (var context = new StoreDBContext(options)){
                CustomerDB _customerDB = new CustomerDB(context);
                var customer1 = _customerDB.GetCustomer("amanboru");
                string username = customer1.UserName;

                Assert.Equal("amanboru", username);
            }
        }
        
        [Fact]
        public void GetCustomerReturnsNull(){
            using (var context = new StoreDBContext(options)){
                CustomerDB _customerDB = new CustomerDB(context);
                var customer1 = _customerDB.GetCustomer("doesntExist");

                Assert.Null(customer1);
            }
        }
        
        [Fact]
        public void AddCustomerWorks(){
            using (var context = new StoreDBContext(options)){
                CustomerDB _customerDB = new CustomerDB(context);
                _customerDB.AddCustomer(new Customer("john doe", "johndoe"));
            }

            using (var assertContext = new StoreDBContext(options)){
                var customer = assertContext.Customers.FirstOrDefault(cust => cust.UserName == "johndoe");
                Assert.NotNull(customer);
                Assert.Equal("john doe", customer.Name);
            }
        }
        
        [Fact]
        public void GetAllLocationsShouldReturnAllLocation(){
            using (var context = new StoreDBContext(options)){
                LocationDB _locationDB = new LocationDB(context);
                var customers = _locationDB.GetAllLocations();
                int count = customers.Count;

                Assert.Equal(1, count);
            }
        }
        
        [Fact]
        public void GetLocationReturnsLocation(){
            using (var context = new StoreDBContext(options)){
                LocationDB _locationDB = new LocationDB(context);
                var location = _locationDB.GetLocationById(1);
                int locationID = location.LocationID;

                Assert.Equal(1, locationID);
            }
        }
        
        [Fact]
        public void GetLocationReturnsNull(){
            using (var context = new StoreDBContext(options)){
                LocationDB _locationDB = new LocationDB(context);
                var location = _locationDB.GetLocationById(2);

                Assert.Null(location);
            }
        }

        [Fact]
        public void AddLocationWorks(){
            using (var context = new StoreDBContext(options)){
                LocationDB _locationDB = new LocationDB(context);
                _locationDB.AddLocation(new Location("new location", "123 new location"));
            }

            using (var assertContext = new StoreDBContext(options)){
                var location = assertContext.Locations.FirstOrDefault(loca => loca.LocationName == "new location" && loca.Address == "123 new location");
                Assert.NotNull(location);
            }
        }

        [Fact]
        public void GetAllOrdersShouldReturnAllOrders(){
            using (var context = new StoreDBContext(options)){
                OrderDB _orderDB = new OrderDB(context);
                var orders = _orderDB.GetAllOrder();
                int count = orders.Count;

                Assert.Equal(1, count);
            }
        }

        
        [Fact]
        public void AddOrderWorks()
        {
            using (var context = new StoreDBContext(options))
            {
                OrderDB _orderBL = new OrderDB(context);
                _orderBL.AddOrder(new Order("Test", 1, DateTime.Now, 0));
            }

            using (var assertContext = new StoreDBContext(options))
            {
                var order = assertContext.Orders.FirstOrDefault(ord => ord.UserName == "Test");
                Assert.NotNull(order);
            }
        }

        [Fact]
        public void GetOrderWorks()
        {
            using (var context = new StoreDBContext(options))
            {
                OrderDB _orderDB = new OrderDB(context);
                var order = _orderDB.GetOrder(1);
                int orderId = order.OrderId;

                Assert.Equal(1, orderId);
            }
        }
        

        [Fact]
        public void GetOrderReturnsNull()
        {
            using (var context = new StoreDBContext(options))
            {
                OrderDB _orderDB = new OrderDB(context);
                var order = _orderDB.GetOrder(5);

                Assert.Null(order);
            }
        }

        private void seed(){
            if (!initialized) { 
                using (var context = new StoreDBContext(options)){

                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    context.Customers.AddRange(
                        new Customer{
                            UserName = "amanboru",
                            Name = "Amanuel Boru",
                        }
                    );

                    context.Locations.AddRange(
                        new Location
                        {
                            LocationID = 1,
                            Address = "123 lovely streat Baltimore, MD",
                            LocationName = "Lovely",
                        }
                     );

                    context.Products.AddRange(
                        new Product
                        {
                            ProductId = 1,
                            ProductName = "Rose",
                            Price = 12.5
                        },
                        new Product
                        {
                            ProductId = 2,
                            ProductName = "Lily",
                            Price = 4.00
                        }
                    );

                    context.Inventories.AddRange(
                        new Inventory
                        {
                            InventoryId = 1,
                            ProductId = 1,
                            LocationId = 1,
                            Quantity = 5
                        },
                        new Inventory
                        {
                            InventoryId = 2,
                            ProductId = 2,
                            LocationId = 1,
                            Quantity = 10
                        }
                    );

                    context.Orders.AddRange(
                        new Order
                        {
                            OrderId = 1,
                            LocationId = 1,
                            UserName = "amanboru",
                            Orderdate = DateTime.Now,
                            Total = 16.50
                        }
                    );

                    context.Items.AddRange(
                        new Item
                        {
                            ItemId = 1,
                            OrderId = 1,
                            ProductId = 1,
                            Quantity = 1
                        },
                        new Item
                        {
                            ItemId = 2,
                            OrderId = 1,
                            ProductId = 2,
                            Quantity = 1
                        }
                    );
                    context.SaveChanges();
                }
            }

            initialized = true;
        }
    }
}