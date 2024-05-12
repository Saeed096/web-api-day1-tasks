using task1.Model;

namespace task1.Repositories
{
    public interface IProductRepository
    {
        public Product GetById(int id);
        public List<Product> GetAll();
        public void addProduct(Product product);
        public void updateProduct(Product product);
        public void deleteProduct(int id);
        public void save();


    }
}