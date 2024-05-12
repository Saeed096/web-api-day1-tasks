using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace task1.Model
{
    public class Context : IdentityDbContext <ApplicationUser>
    {
        public Context(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Product> products {  get; set; }
        public DbSet<Category> categories {  get; set; }
    }
}
