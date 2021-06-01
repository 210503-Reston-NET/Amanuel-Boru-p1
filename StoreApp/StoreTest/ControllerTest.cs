using Microsoft.AspNetCore.Mvc;
using Moq;
using StoreBL;
using StoreModels;
using StoreWebUI.Controllers;
using StoreWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StoreTest
{
    public class ControllerTest
    {
        [Fact]
        public void CustomerControllerTestShoudWork()
        {
            var mockBL = new Mock<ICustomerBL>();
            mockBL.Setup(x => x.GetAllCustomers()).Returns(
                new List<Customer>()
                {
                    new Customer("test1", "Test one"),
                    new Customer("test1", "Test two")
                }
                );

            var mockBL2 = new Mock<IOrderBL>();
            mockBL2.Setup(x => x.GetAllOrder()).Returns(
                new List<Order>()
                {
                    new Order("test1", 1, DateTime.Now, 0)
                }
                
                );

            var controller = new CustomerController(mockBL.Object, mockBL2.Object);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<CustomerVM>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void StoresControllerShouldWork()
        {
            var mockBL = new Mock<ILocationBL>();
            mockBL.Setup(x => x.GetAllLocations()).Returns(
                new List<Location>()
                {
                    new Location("loation one", "123 address"),
                    new Location("loation two", "456 address")
                }
                );

            var mockBL2 = new Mock<IOrderBL>();
            mockBL2.Setup(x => x.GetAllOrder()).Returns(
                new List<Order>()
                {
                    new Order("test1", 1, DateTime.Now, 0)
                }

                );

            var controller = new StoresController(mockBL.Object, mockBL2.Object);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<LocationVM>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void ProductControllerShouldWork()
        {
            var mockBL = new Mock<IProductBL>();
            mockBL.Setup(x => x.GetAllProducts()).Returns(
                new List<Product>()
                {
                    new Product("Lily", 12.5),
                    new Product("Rose", 5.5)
                }
                );

            var controller = new ProductController(mockBL.Object);
            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ProductVM>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());

        }

        [Fact]
        public void OrderControllerShouldWork()
        {
            var mockBL = new Mock<IOrderBL>();
            mockBL.Setup(x => x.GetAllOrder()).Returns(
                new List<Order>()
                {
                    new Order("test1", 1, DateTime.Now, 0)
                }

                );

            var mockBL2 = new Mock<ICustomerBL>();
            mockBL2.Setup(x => x.GetAllCustomers()).Returns(
                new List<Customer>()
                {
                    new Customer("test1", "Test one"),
                    new Customer("test1", "Test two")
                }
                );

            var mockBL3 = new Mock<ILocationBL>();
            mockBL3.Setup(x => x.GetAllLocations()).Returns(
                new List<Location>()
                {
                    new Location("loation one", "123 address"),
                    new Location("loation two", "456 address")
                }
                );

            var controller = new OrderController(mockBL.Object, mockBL2.Object, mockBL3.Object);
            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<OrderVM>>(viewResult.ViewData.Model);
            Assert.Equal(1, model.Count());
        }
    }
}
