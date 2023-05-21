using Microsoft.Extensions.Options;
using Utility.Project.Core.Model.AppSettings;

namespace Utility.Project.API.Modules
{
    public static class OptionsConfigureModule
    {
        public static void AddOptionsConfigure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoDbSettings>(sp => sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
        }
    }
}
