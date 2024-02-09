using Generics.Data;
using Microsoft.EntityFrameworkCore;

namespace Generics.ExtensionMethods
{
    public static class DbContextExtensionMethods
    {
        public static IServiceCollection AddDbContextConnection(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
