using System.Collections.Generic;
using System.Linq;

namespace Demo.DAL
{
    public class CategoryRepositry
    {
        Kaveh01Entities1 context;
        public CategoryRepositry()
        {
            context = new Kaveh01Entities1();
        }

        public List<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public Category FindByName(string name)
        {
            return context.Categories.Single(a => a.Name == name);
        }
    }
}
