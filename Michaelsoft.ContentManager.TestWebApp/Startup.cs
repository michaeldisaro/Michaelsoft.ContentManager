using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Michaelsoft.ContentManager.Client.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Michaelsoft.ContentManager.TestWebApp
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddContentManager(Configuration);
            services.AddRazorPages()
                    .AddRazorPagesOptions(options =>
                    {
                        /* ROUTES */
                        options.Conventions.AddAreaPageRoute("Site", "/Index", "");
                    })
                    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                    .AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.AddContentManager();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

        }

    }
}