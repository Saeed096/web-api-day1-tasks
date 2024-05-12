using Microsoft.EntityFrameworkCore;
using task1.Model;

namespace task1.Repositories
{
    public enum categoryInclude
    {
        products,
        none
    }
    public class CategoryRepository : ICategoryRepository
    {
      
        private readonly Context context;

        public CategoryRepository(Context _context)
        {
            context = _context;
        }

        public void addCategory(Category category)
        {
            context.Add(category);
        }

        public Category Get(Func< Category,bool> filter , categoryInclude CI = categoryInclude.none)
        {
            Category category = new Category();
            switch(CI)
            {
                case categoryInclude.products:
                    category = context.categories.Include(c=>c.Products).FirstOrDefault(filter);
                    break;

                default:
                    category = context.categories.FirstOrDefault(filter);
                    break;
            }
            return category;
        }

        public void save() 
        {
            context.SaveChanges();
        }
    }
}
