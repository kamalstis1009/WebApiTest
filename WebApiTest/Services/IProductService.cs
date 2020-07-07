﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationTest.Models;

namespace WebApplicationTest.Services
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        Task<Product> GetProductById2(int id);
        Product AddProduct(Product item);
        Product UpdateProduct(Product item);
        Product DeleteProduct(int id);
        string Index(List<Product> _items);
    }
}
