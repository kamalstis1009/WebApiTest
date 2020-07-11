using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationTest.Models;

namespace WebApiTest.DTO
{
    public class ProductRepository : IProductRepository
    {
        private readonly SQLDatabaseContext _context;

        public ProductRepository(SQLDatabaseContext context)
        {
            _context = context;
        }

        //======================================================| Get all objects
        public List<Product> GetAllProducts()
        {
            //return _context.Database.SqlQuery<Product>("exec Product").ToList<Product>(); //Procedure query
            //OR
            //return _context.Products.SqlQuery("Select * from Products").ToList<Product>();
            //OR
            return _context.Products.ToList();
        }

        //======================================================| Get object by Id
        public Product GetProductById(int id)
        {
            //var query = (from s in _context.HRM_PersonalInformations where s.OCODE == oCode && s.EID == eId select new HRM_PersonalInformations{ JoiningDate = s.JoiningDate}).FirstOrDefault();
            //OR
            return _context.Products.FirstOrDefault(model => model.ID == id);
        }

        public Task<Product> GetProductById2(int id)
        {
            //return _context.Products.FirstOrDefault(model => model.ID == id);
            return _context.Products.Where(index => index.ID == id).SingleAsync();
        }

        //======================================================| Add
        public Product AddProduct(Product item)
        {
            var response = _context.Products.Add(item);
            //_context.SaveChangesAsync();
            _context.SaveChanges();
            if (response != null)
            {
                return item;
            }
            return null;
        }

        //======================================================| Put/Update
        public Product UpdateProduct(Product item)
        {
            Product model = _context.Products.FirstOrDefault(index => index.ID == item.ID);
            if (model != null)
            {
                model.Name = item.Name;
                model.Brand = item.Brand;
                //_context.SaveChangesAsync();
                _context.SaveChanges();
                return model;
            }
            return null;
        }

        //======================================================| Delete
        public Product DeleteProduct(int id)
        {
            //Help: www.entityframeworktutorial.net/EntityFramework4.3/raw-sql-query-in-entity-framework.aspx
            //int response = _context.Database.ExecuteSqlCommand("delete from Products where ID=" + id);
            //OR

            var item = GetProductById(id);
            if (item != null)
            {
                var response = _context.Products.Remove(item);
                //_context.SaveChangesAsync();
                _context.SaveChanges();
                if (response != null)
                {
                    return item;
                }
                return null;
            }
            return null;
        }

        //======================================================| Index
        public string Index(List<Product> _items)
        {
            foreach (Product item in _items)
            {
                _context.Products.Add(item);
                _context.SaveChanges();
            }
            return "success";
        }

    }
}
