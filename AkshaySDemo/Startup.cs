using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AkshaySDemoSQLServer.Data;
using AkshaySDemoMySQL.Data;
using AkshaySDemoGoogleFireStoreDB.DataAccess;
using AkshaySDemoPostgreSQL.Data;
using AkshaySDbDiscoverySQLServer;

namespace AkshaySDemo
{
    public class Startup
    {
        public static DbTypeQueries dbTypeQueries = new DbTypeQueries();

        public Startup(IConfiguration configuration)
        {
            AkshaySDemoContext.ConnectionString = configuration.GetConnectionString("AkshaySDemoDBConnStr");
            AkshaySDemoMySQLContext.ConnectionString = configuration.GetConnectionString("MySQLConnStr");
            AkshaySDemoPostgreSQLContext.ConnectionString = configuration.GetConnectionString("PostgreSQLConnStr");
            ChefDataAccessLayer.GoogleFirestoreDBJsonString = configuration.GetConnectionString("GoogleFirestoreDBJsonString");
            dbTypeQueries.ConnectionString = configuration.GetConnectionString("AkshaySDemoDBConnStr");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/AkshaySDemoSPA");
            });
        }
    }
}
