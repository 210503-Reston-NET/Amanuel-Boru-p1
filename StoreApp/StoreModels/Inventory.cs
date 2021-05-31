using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreModels
{
    public class Inventory
    {
        public Inventory()
        {
        }
        public Inventory(int locationId, int product, int quantity)
        {
            LocationId = locationId;
            ProductId = product;
            Quantity = quantity;
        }

        public Inventory(int id, int locationId, int product, int quantity)
        {
            InventoryId = id;
            LocationId = locationId;
            ProductId = product;
            Quantity = quantity;
        }

        public Inventory(int locationId, int product)
        {
            LocationId = locationId;
            ProductId = product;
        }

        public int InventoryId { get; set; }
        public int LocationId { get; set; }

        [ForeignKey("Product")]
        [Required]
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public bool Equals(Inventory inventory){
            return this.ProductId.Equals(inventory.ProductId);
        }
        public override string ToString()
        {
            return $" {ProductId} \t Quantity: {Quantity}";
        }
    }
}