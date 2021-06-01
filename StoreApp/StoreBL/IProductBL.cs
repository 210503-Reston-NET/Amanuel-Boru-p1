using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreModels;
using StoreDL;

namespace StoreBL
{
    public interface IProductBL
    {
        Product AddProduct(Product newProduct);

        bool ProductExists(int id);

        List<Product> GetAllProducts();

        Product GetProduct(int id);

        Product UpdateProduct(Product product);

        Product DeleteProduct(Product product);
    }
}
