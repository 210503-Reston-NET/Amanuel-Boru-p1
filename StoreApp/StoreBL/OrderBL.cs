using StoreDL;
using StoreModels;
using System.Collections.Generic;
using System;

namespace StoreBL
{
    public class OrderBL : IOrderBL
    {
        private OrderDB _orderDB;
        private ProductsDB _productDB;
        public OrderBL(OrderDB orderDB, ProductsDB productDB)
        {
            _orderDB = orderDB;
            _productDB = productDB;
        }

        public Order AddOreder(Order newOrder){
            return _orderDB.AddOrder(newOrder);
        }

        public Order GetOrder(int id)
        {
            return _orderDB.GetOrder(id);
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

        public List<Item> GetItems(int id)
        {
            return _orderDB.GetItems(id);
        }

        public Item GetItem(int id)
        {
            return _orderDB.GetItem(id);
        }

        public List<Order> GetAllOrder(){
            return _orderDB.GetAllOrder();
        }

        public List<Order> CustomerOrdersBydate(Customer customer){
            return _orderDB.GetCustomerOrderByDate(customer);
        }

        public List<Order> CustomerOrdersBydateDes(Customer customer)
        {
            return _orderDB.GetCustomerOrderByDateDes(customer);
        }

        public List<Order> CustomerOrdersByTotal(Customer customer){
            return _orderDB.GetCustomerOrderByTotal(customer);
        }

        public List<Order> CustomerOrdersByTotalDes(Customer customer)
        {
            return _orderDB.GetCustomerOrderByTotalDes(customer);
        }

        public List<Order> LocationOrdersBydate(int location){
            return _orderDB.LocationOrdersBydate(location);
        }

        public List<Order> LocationOrdersBydateDes(int location)
        {
            return _orderDB.LocationOrdersBydateDes(location);
        }

        public List<Order> LocationOrdersByTotal(int location){
            return _orderDB.LocationOrdersByTotal(location);
        }

        public List<Order> LocationOrdersByTotalDes(int location)
        {
            return _orderDB.LocationOrdersByTotalDes(location);
        }

        public void AddItem(Item item)
        {
            _orderDB.AddItem(item);
        }

        public Item DeleteItem(int id)
        {
            return _orderDB.DeleteItem(id);
        }

        public void UpdateOrder(Order order)
        {
            _orderDB.UpdateOrder(order);
        }

        public void DeleteOrder(int id)
        {
            _orderDB.DeleteOrder(id);
        }
    }
}