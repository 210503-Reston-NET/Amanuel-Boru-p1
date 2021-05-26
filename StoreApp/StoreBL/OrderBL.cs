using StoreDL;
using StoreModels;
using System.Collections.Generic;
using System;

namespace StoreBL
{
    public class OrderBL
    {
        private OrderDB _orderDB;
        private ProductsDB _productDB;
        public OrderBL(OrderDB orderDB, ProductsDB productDB)
        {
            _orderDB = orderDB;
        }

        public Order AddOreder(Order newOrder, Location location){
            double total = calculateTotal(newOrder);
            newOrder.Total = total;
            return _orderDB.AddOrder(newOrder);
        }

        private double calculateTotal(Order newOrder)
        {
            List<Item> items = newOrder.Items;
            int productId;
            double price;
            double total = 0;
            foreach(Item item in items){
                productId = item.ProductId;
                price = _productDB.GetProduct(productId).Price;
                total += price*item.Quantity;
            }

            return total;
        }

        public List<Order> GetAllOrder(){
            return _orderDB.GetAllOrder();
        }

        public List<Order> CustomerOrdersBydate(Customer customer){
            return _orderDB.GetCustomerOrderByDate(customer);
        }

        public List<Order> CustomerOrdersByTotal(Customer customer){
            return _orderDB.GetCustomerOrderByTotal(customer);
        }

        public List<Order> LocationOrdersBydate(Location location){
            return _orderDB.LocationOrdersBydate(location);
        }

        public List<Order> LocationOrdersByTotal(Location location){
            return _orderDB.LocationOrdersByTotal(location);
        }
    }
}