using Microsoft.EntityFrameworkCore;
using WebAPI.DAL;
using WebAPI.Extensibility;
using WebAPI.Repositories;

namespace WebAPI.IoC
{
    public static class Module
    {
        internal static void RegisterIoC(this IServiceCollection services)
        {
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("Database"));
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
