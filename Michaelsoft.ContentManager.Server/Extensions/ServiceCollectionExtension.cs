using Michaelsoft.ContentManager.Server.Services;
using Michaelsoft.ContentManager.Server.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Michaelsoft.ContentManager.Server.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static void AddEncryptionService(this IServiceCollection services,
                                                IConfiguration configuration)
        {
            services.Configure<EncryptionSettings>
                (configuration.GetSection(nameof(EncryptionSettings)));

            services.AddSingleton<IEncryptionSettings>
                (sp => sp.GetRequiredService<IOptions<EncryptionSettings>>().Value);

            services.AddSingleton<DatabaseEncryptionService>();

        }

        public static void AddContentService(this IServiceCollection services,
                                             IConfiguration configuration)
        {
            services.Configure<ContentStoreDatabaseSettings>
                (configuration.GetSection(nameof(ContentStoreDatabaseSettings)));

            services.AddSingleton<IContentStoreDatabaseSettings>
                (sp => sp.GetRequiredService<IOptions<ContentStoreDatabaseSettings>>().Value);

            services.AddSingleton<ContentService>();
        }

        public static void AddTokenService(this IServiceCollection services,
                                           IConfiguration configuration)
        {
            services.Configure<TokenStoreDatabaseSettings>
                (configuration.GetSection(nameof(TokenStoreDatabaseSettings)));

            services.AddSingleton<ITokenStoreDatabaseSettings>
                (sp => sp.GetRequiredService<IOptions<TokenStoreDatabaseSettings>>().Value);

            services.AddSingleton<TokenService>();
        }

    }
}