using System.Threading;
using Michaelsoft.ContentManager.Common.BaseClasses;
using Microsoft.AspNetCore.Builder;

namespace Michaelsoft.ContentManager.Client.Extensions
{
    public static class ApplicationBuilderExtension
    {

        public static void AddContentManager(this IApplicationBuilder app)
        {
            InjectableServicesBaseStaticClass.Services = app.ApplicationServices;
            app.UseRequestLocalization();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
            });
        }

    }
}