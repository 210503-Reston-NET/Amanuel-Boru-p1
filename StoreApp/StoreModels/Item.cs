using System.Text.Json.Serialization;
namespace StoreModels
{    /// <summary>
    /// This data structure models a product and its quantity. The quantity was separated from the product as it could vary from orders and locations.  
    /// </summary>
    public class Item
    {
        public Item()
        {
        }
        public Item(int orderId, int product, int quantity)
        {
            OrderId = orderId;
            ProductId = product;
            Quantity = quantity;
        }

        public Item(int product, int quantity)
        {
            ProductId = product;
            Quantity = quantity;
        }

        public Item(int itemId, int orderId, int product, int quantity)
        {
            ItemId = itemId;
            OrderId = orderId;
            ProductId = product;
            Quantity = quantity;
        }

        public int ItemId { get; set; }

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public bool Equals(Item newItem){
            return this.ProductId.Equals(newItem.ProductId) && Quantity.Equals(newItem.Quantity);
        }
        public override string ToString()
        {
            return $" {ProductId} \t Quantity: {Quantity}";
        }
    }
}