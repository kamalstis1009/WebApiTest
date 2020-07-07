using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiTest.DTO;
using WebApplicationTest.Models;

namespace WebApplicationTest.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository; //dependency injection
        }

        //======================================================| Get all objects
        public List<Product> GetAllProducts()
        {
            return _repository.GetAllProducts();
        }

        //======================================================| Get object by Id
        public Product GetProductById(int id)
        {
            return _repository.GetProductById(id);
        }

        public Task<Product> GetProductById2(int id)
        {
            return _repository.GetProductById2(id);
        }

        //======================================================| Add
        public Product AddProduct(Product item)
        {
            return _repository.AddProduct(item);
        }

        //======================================================| Put/Update
        public Product UpdateProduct(Product item)
        {
            return _repository.UpdateProduct(item);
        }

        //======================================================| Delete
        public Product DeleteProduct(int id)
        {
            return _repository.DeleteProduct(id);
        }

        //======================================================| index
        public string Index(List<Product> _items)
        {
            return _repository.Index(_items);
        }

    }
}
