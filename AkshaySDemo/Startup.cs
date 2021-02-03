using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AkshaySDemoMySQL.Data;
using AkshaySDemoGoogleFireStoreDB.DataAccess;
using AkshaySDemoPostgreSQL.Data;
using AkshaySDemoOracle.Data;

namespace AkshaySDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            AkshaySDemoMySQLContext.ConnectionString = configuration.GetConnectionString("MySQLConnStr");
            AkshaySDemoPostgreSQLContext.ConnectionString = configuration.GetConnectionString("PostgreSQLConnStr");
            AkshaySDemoOracleContext.ConnectionString = configuration.GetConnectionString("OracleConnStr");
            ChefDataAccessLayer.GoogleApisOAuthJsonString = configuration.GetValue<string>("GoogleApisOAuthJsonString");
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
