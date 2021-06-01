using System;
using StoreDL;
using StoreModels;
using System.Collections.Generic;

namespace StoreBL
{
    public class ProductBL : IProductBL
    {
        private ProductsDB _ProductsDB;

        public ProductBL(ProductsDB ProductsDB)
        {
            _ProductsDB = ProductsDB;
        }
        public Product AddProduct(Product newProduct){
            return _ProductsDB.AddProduct(newProduct);
        }

        public bool ProductExists(int id){
            if (this.GetProduct(id) != null) return true;

            return false;
        }

        public List<Product> GetAllProducts(){
            return _ProductsDB.GetAllProducts();
        }

        public Product GetProduct(int id){
            return _ProductsDB.GetProduct(id);
        }

        public Product UpdateProduct(Product product)
        {
            return _ProductsDB.UpdateProduct(product);
        }

        public Product DeleteProduct(Product product)
        {
            Product toBeDeleted = _ProductsDB.DeleteProduct(product);
            if (toBeDeleted != null)  return _ProductsDB.DeleteProduct(toBeDeleted);
            else
            {
                throw new Exception("There is no product to delete");
            }
        }
    }
}