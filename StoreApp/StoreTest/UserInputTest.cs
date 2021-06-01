using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreWebUI.Models;
using Xunit;
using StoreModels;

namespace StoreTest
{
    public class UserInputTest
    {
        [Fact]
        public void CustomerShouldAccptValidInput()
        {
            string name = "Test Test";
            string username = "username";

            CustomerVM customer = new CustomerVM();
            customer.name = name;
            customer.username = username;

            Assert.Equal(name, customer.name);
            Assert.Equal(username, customer.username);
        }



        [Fact]
        public void LocationShouldAccptValidInput()
        {
            string name = "Test Test";
            string address = "123 Address";

            LocationVM location = new LocationVM();
            location.LocationName = name;
            location.Address = address;

            Assert.Equal(name, location.LocationName);
            Assert.Equal(address, location.Address);
        }

    }
}
