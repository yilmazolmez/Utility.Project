using Utility.Project.Core.Data.Abstraction.Mongo;
using Utility.Project.Core.Data.Concrete.Mongo;

namespace Utility.Project.API.Modules
{
    public static class DataAccessModule
    {
        public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
        }
    }
}
