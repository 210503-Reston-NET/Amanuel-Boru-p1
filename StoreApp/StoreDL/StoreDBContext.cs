using Microsoft.EntityFrameworkCore;
using StoreModels;
namespace StoreDL
{
    public class StoreDBContext :DbContext
    {
        public StoreDBContext() : base()
        {
            
        }
        public StoreDBContext(DbContextOptions options): base(options)
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Manager> Managers { get; set; } 
        public DbSet<Item> Items { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasKey(customer => customer.UserName);
            modelBuilder.Entity<Manager>()
                .HasKey(manager => manager.UserName);
            modelBuilder.Entity<Item>()
                .Property(item => item.ItemId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Location>()
                .Property(location => location.LocationID)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Inventory>()
                .Property(invetory => invetory.InventoryId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Order>()
                .Property(order => order.OrderId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>()
                .Property(product => product.ProductId)
                .ValueGeneratedOnAdd();
        }
    }
}