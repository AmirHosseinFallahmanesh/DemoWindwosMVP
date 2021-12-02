using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL
{
    public class ProductRepository
    {
        Kaveh01Entities1 context;
        public ProductRepository()
        {
            context = new Kaveh01Entities1();
        }
        public List<Product> GetAll()
        {
            return context.Products.AsNoTracking(). ToList();
        }
        public void Add(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public Product FindByName(string name)
        {
            return context.Products.Single(a => a.Name == name);
        }


        public Product Get(int id)
        {
            return context.Products.Find(id);
        }

        public void Update(Product product)
        {
            context.Entry(product).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }

        public List<Product>FilterPrice(decimal min,decimal max)
        {
            return context.Products.Where(a => a.Price >= min && a.Price < max).ToList();
        }


    }
}
