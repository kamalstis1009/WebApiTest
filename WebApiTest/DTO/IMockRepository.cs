using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationTest.Models;

namespace WebApiTest.DTO
{
    public interface IMockRepository
    {
        //bool SaveChanges();
        //IEnumerable<Product> GetAllProducts();
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        Product AddProduct(Product item);
        Product UpdateProduct(Product item);
        int DeleteProduct(int id);

    }
}
