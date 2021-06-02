using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreModels;
using StoreDL;

namespace StoreBL
{
    public interface IOrderBL
    {
        Order AddOreder(Order newOrder);

        Order GetOrder(int id);


        List<Item> GetItems(int id);

        Item GetItem(int id);

        List<Order> GetAllOrder();

        List<Order> CustomerOrdersBydate(Customer customer);

        List<Order> CustomerOrdersBydateDes(Customer customer);

        List<Order> CustomerOrdersByTotal(Customer customer);

        List<Order> CustomerOrdersByTotalDes(Customer customer);

        List<Order> LocationOrdersBydate(int location);

        List<Order> LocationOrdersBydateDes(int location);

        List<Order> LocationOrdersByTotal(int location);

        List<Order> LocationOrdersByTotalDes(int location);

        void AddItem(Item item);

        Item DeleteItem(int id);

        void UpdateOrder(Order order);

        void DeleteOrder(int id);
    }
}
