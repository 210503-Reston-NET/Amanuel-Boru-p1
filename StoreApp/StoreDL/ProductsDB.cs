using System.Collections.Generic;
using StoreModels;
using System.Linq;

namespace StoreDL
{
    public class ProductsDB
    {
        private StoreDBContext _context;
        public ProductsDB(StoreDBContext context)
        {
            _context = context;
        }

        public Product AddProduct(Product newProduct){
            _context.Products.Add(newProduct);

            _context.SaveChanges();
            return newProduct;
        }
        public List<Product> GetAllProducts(){
            List<Product> Products = _context.Products
                .Select( product => new Product(product.ProductId, product.ProductName, product.Price))
                .ToList();
            
            return Products;
        }

        public Product GetProduct(int id){
            Product found = _context.Products.FirstOrDefault(product =>  product.ProductId == id);

            if (found == null) return null;
            else{
                return new Product(found.ProductId, found.ProductName, found.Price);
            }
        }
    }
}