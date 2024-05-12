using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using task1.Model;

namespace task1.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context context;

        public ProductRepository(Context _context)
        {
            context = _context;
        }

        public Product GetById(int id) 
        {
            
          Product product = context.products.FirstOrDefault(p => p.Id == id);
            if(product != null)
            context.Entry(product).State = EntityState.Detached;       // right???? right
            return product;
        } 

        public List<Product> GetAll() 
        {
           return context.products.ToList(); 
        }

        public void addProduct(Product product)
        {
            context.Add(product);
        }

        public void updateProduct(Product product) 
        {
           //Product pro = context.products.First(p => p.Id == product.Id);
           // if (pro != null)
                context.Update(product);
           
        }

        public void deleteProduct(int id) 
        {
            Product product = context.products.First(p => p.Id == id);
            context.products.Remove(product);
        }
        public void save()
        {
            context.SaveChanges();
        }
    }

 
}
