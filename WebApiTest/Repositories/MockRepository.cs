using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationTest.Models;

namespace WebApiTest.DTO
{
    public class MockRepository : IMockRepository
    {
        private readonly List<Product> _items = new List<Product>
            {
                new Product{ID=0, Name="Nokia 730 Dual", Brand="Nokia"},
                new Product{ID=1, Name="iPhone 6s", Brand="Apple"},
                new Product{ID=2, Name="Galaxy S6", Brand="Samsung"}
            };


        public List<Product> GetAllProducts()
        {
            return _items;
        }

        /*public IEnumerable<Product> GetAllProducts()
        {
            return _items;
        }*/

        public Product GetProductById(int id)
        {
            /*Product item = null;
            foreach (Product p in _items)
            {
                if (p.ID == id)
                {
                    item = p;
                }
            }*/

            //OR linq
            var item = _items.Find(x => x.ID == id);
            //OR
            //var item = _items.FirstOrDefault(x => x.ID == id);
            //OR
            //var item = from obj in _items select new { obj.Name, obj.Brand };

            return item;
        }


        public Product AddProduct(Product item)
        {
            _items.Add(item);
            return item;
        }

        public int DeleteProduct(int id)
        {
            _items.RemoveAll(index => index.ID == id);

            //OR
            /*for (int i = items.Count - 1; i >= 0; i--)
            {
                if (items[i].ID == id)
                {
                    items.RemoveAt(i);
                }
            }*/

            //OR linq
            /*var item = items.SingleOrDefault(x => x.ID == id);
            if (item != null)
                items.Remove(item);*/

            return id;
        }

        public Product UpdateProduct(Product item)
        {
            for (var index = _items.Count - 1; index >= 0; index--)
            {
                if (_items[index].ID == item.ID)
                {
                    _items[index] = item;
                }
            }
            return item;
        }
    }
}
