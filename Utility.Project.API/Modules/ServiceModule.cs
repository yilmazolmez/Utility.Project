using Utility.Project.Business.Service.Abstraction.Mongo;
using Utility.Project.Business.Service.Concrete.Mongo;

namespace Utility.Project.API.Modules
{
    public static class ServiceModule
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
