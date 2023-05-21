using Utility.Project.Core.Model.AppSettings;

namespace Utility.Project.API.Modules
{
    public static class SettingsModule
    {
        public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
        }
    }
}
