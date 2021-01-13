using System;
using System.Collections.Generic;
using System.Globalization;
using Askmethat.Aspnet.JsonLocalizer.Extensions;
using Askmethat.Aspnet.JsonLocalizer.JsonOptions;
using Michaelsoft.ContentManager.Client.Interfaces;
using Michaelsoft.ContentManager.Client.Services;
using Michaelsoft.ContentManager.Client.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Michaelsoft.ContentManager.Client.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static void AddContentManager(this IServiceCollection services,
                                             IConfiguration configuration)
        {
            services.Configure<ContentManagerClientSettings>
                (configuration.GetSection(nameof(ContentManagerClientSettings)));

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("it-IT"),
                    new CultureInfo("en-US")
                };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddJsonLocalization(options =>
            {
                options.LocalizationMode = LocalizationMode.I18n;
                options.UseBaseName = false;
                options.IsAbsolutePath = false;
                options.ResourcesPath = "Resources/";
                options.CacheDuration = TimeSpan.FromMinutes(60 * 24);
                options.SupportedCultureInfos = new HashSet<CultureInfo>
                {
                    new CultureInfo("it-IT"),
                    new CultureInfo("en-US")
                };
            });

            services.AddSingleton<IContentManagerClientSettings>
                (sp => sp.GetRequiredService<IOptions<ContentManagerClientSettings>>().Value);

            services.AddHttpClient();
            services.AddHttpContextAccessor();

            services.AddSingleton<IContentManagerAuthenticationApiService, ContentManagerAuthenticationApiService>();
            services.AddSingleton<IContentManagerContentApiService, ContentManagerContentApiService>();
            services.AddSingleton<IContentManagerMediaApiService, ContentManagerMediaApiService>();

            services.AddRazorPages()
                    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                    .AddDataAnnotationsLocalization()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddServerSideBlazor();
        }

    }
}