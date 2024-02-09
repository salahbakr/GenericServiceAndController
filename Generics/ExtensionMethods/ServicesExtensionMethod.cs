using Generics.IServices;
using Generics.Mapping;
using Generics.Services;

namespace Generics.ExtensionMethods
{
    public static class ServicesExtensionMethod
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped(typeof(IGenericService<,,>), typeof(GenericService<,,>));

            return services;
        }
    }
}
