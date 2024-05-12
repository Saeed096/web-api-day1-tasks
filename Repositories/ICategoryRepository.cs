using task1.Model;
using static task1.Repositories.CategoryRepository;

namespace task1.Repositories
{
    public interface ICategoryRepository
    {

        public Category Get(Func<Category, bool> filter, categoryInclude CI);
        public void addCategory(Category category);
        public void save(); 


    }
}