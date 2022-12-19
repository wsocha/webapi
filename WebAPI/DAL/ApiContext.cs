using Microsoft.EntityFrameworkCore;
using WebAPI.Models.Entities;

namespace WebAPI.DAL
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
