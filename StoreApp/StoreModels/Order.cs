using System;
using System.Collections.Generic;

namespace StoreModels
{    
    /// <summary>
    /// This class should contain all the fields and properties that define a customer order. 
    /// </summary>
    public class Order
    {
        public Order()
        {
        }
        public Order(string customer, int location, List<Item> items)
        {
            UserName = customer;
            LocationId = location;
            Orderdate = DateTime.Now;
            Items = items;
        }


        public Order(string customer, int location, DateTime orderdate , List<Item> items, double total)
        {
            UserName = customer;
            LocationId = location;
            Orderdate = orderdate;
            Items = items;
            Total = total;
        }

        public Order(int id, string customer, int location, DateTime orderdate, double total)
        {
            OrderId = id;
            UserName = customer;
            LocationId = location;
            Orderdate = orderdate;
            Total = total;
        }

        public Order(string customer, int location, DateTime orderdate, double total)
        {
            UserName = customer;
            LocationId = location;
            Orderdate = orderdate;
            Total = total;
        }

        public int OrderId { get; set; }
        public string UserName { get; set; }
        public int LocationId { get; set; }
        public DateTime Orderdate { get; set;}
        public List<Item> Items { get; set; }
        public double Total { get; set; }

        //TODO: add a property for the order items

        public override string ToString()
        {
            return $" customer name: {UserName} \t\t Total: {Total} \t {Orderdate}";
        }
    }
}