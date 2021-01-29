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
            services.Configure<SymmetricEncryptionSettings>
                (configuration.GetSection(nameof(SymmetricEncryptionSettings)));

            services.AddSingleton<ISymmetricEncryptionSettings>
                (sp => sp.GetRequiredService<IOptions<SymmetricEncryptionSettings>>().Value);

            services.Configure<AsymmetricEncryptionSettings>
                (configuration.GetSection(nameof(AsymmetricEncryptionSettings)));

            services.AddSingleton<IAsymmetricEncryptionSettings>
                (sp => sp.GetRequiredService<IOptions<AsymmetricEncryptionSettings>>().Value);

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

        public static void AddMediaStorageSetting(this IServiceCollection services,
                                                  IConfiguration configuration)
        {
            services.Configure<MediaStorageSettings>
                (configuration.GetSection(nameof(MediaStorageSettings)));

            services.AddSingleton<IMediaStorageSettings>
                (sp => sp.GetRequiredService<IOptions<MediaStorageSettings>>().Value);
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