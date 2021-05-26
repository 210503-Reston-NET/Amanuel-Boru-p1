using System;
using StoreDL;
using StoreModels;
using System.Collections.Generic;

namespace StoreBL
{
    public class ProductBL
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
    }
}