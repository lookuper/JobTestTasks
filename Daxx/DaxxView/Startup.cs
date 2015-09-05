using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.Runtime;
using Microsoft.Framework.Logging;
using Microsoft.AspNet.Diagnostics;
using Microsoft.Data.Entity;

namespace DaxxView
{
    using Microsoft.Data.Entity;

    namespace MovieAngularJSApp.Models
    {
        public class MoviesAppContext : DbContext
        {

            public DbSet<string> Movies { get; set; }

        }
    }

    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var configBuilder = new ConfigurationBuilder(appEnv.ApplicationBasePath);

            configBuilder.AddJsonFile("config.json");
            configBuilder.AddEnvironmentVariables();

            Configuration = configBuilder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<DbContext>();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory)
        {
            // Configure the HTTP request pipeline.

            // Add the console logger.
            loggerfactory.AddConsole();

            // Add the following to the request pipeline only in development environment.
            if (env.IsEnvironment("Development"))
            {
                app.UseBrowserLink();
                app.UseErrorPage(ErrorPageOptions.ShowAll);
            }
            else
            {
                // Add Error handling middleware which catches all application specific errors and
                // send the request to the following path or controller action.
                app.UseErrorHandler("/Home/Error");
            }

            // Add static files to the request pipeline.
            app.UseStaticFiles();

            // Add MVC to the request pipeline.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                // Uncomment the following line to add a route for porting Web API 2 controllers.
                // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });


            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Hello world!");
            });
        }

        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddMvc();
        //    new Configuration()
        //    //   .AddJsonFile("config.json")
        //    //   .AddEnvironmentVariables();
        //}

        //public void Configure(IApplicationBuilder app)
        //{
        //    //app.UseMvc();

        //    app.Use(async (context, next) =>
        //    {
        //        await context.Response.WriteAsync("Hello world!");
        //    });
        //}
    }
}
