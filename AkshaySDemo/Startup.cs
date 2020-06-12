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
        public static string ERUDemoDBConnStr = "Server=192.168.29.101;Database=AkshaySDemoDB;User ID=sa;Password=P@ssword123;";
        public static string AkshaySDemoDBConnStr = "Server=localhost;Database=AkshaySDemoDB;Trusted_Connection=True;";
        public static string DefaultConnection = "Server=tcp:akshaysdemo20200506070129dbserver.database.windows.net,1433;Initial Catalog=AkshaySDemo20200506070129_db;Persist Security Info=False;User ID=AkshaySDemoDBAdmin;Password=P@ssword123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static string OlorinDemoDBConnStr = "Server=192.168.29.250;Database=AkshaySDemoDB;User Id=sa;Password=P@ssword123";
        public static string MySQLConnStr = "Server=localhost;Database=akshaysdemodb;User=root;Password=P@ssword123;TreatTinyAsBoolean=true;";
        public static string PostgreSQLConnStr = "Host=localhost;Database=akshaysdemodb;Username=postgres;Password=P@ssword123";
        public static string GoogleFirestoreDBJsonString = "{ \"type\": \"service_account\", \"project_id\": \"akshaysdemo\", \"private_key_id\": \"4eb4511f3238abf9456cbe93bd1c989e5145cacf\", \"private_key\": \"-----BEGIN PRIVATE KEY-----\nMIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQCl02nIZZhlwu3Z\ngjvkuR+G3zSptc2B1OETI1i5u7Y45SnkflT1wXn4WOmopcQF25YMstalUwwOD8on\nY1Xf7Vs44izXnOoT1gZtXn4KwXTFnFcUxndtESUpTB2DQ05UU7ep/gg0HW4CO4fk\nksFZhkdOhxDdtpbxWXX4igagVr/oRTiPblUMF+zBB1elmHO8mY9PxXwG1jIFS2cM\niRdYjwhULJ4Zc2z/pZlBzh7WlPNAjUb/EBoWCDIAFCHXm+Afldy0fv1jxsAmXQFW\nrEsq1ji2iHCTl0wthBws5cu4jsfOWfAhgo7um2SLCAOWg+Bi9pjR668KuzGGzxME\nZEI4733bAgMBAAECggEADgdeuA3WrY7aLgz7u2C1/BdyDlF+u1ciweQho3gjKGXd\n/DviPPaxtoLyV/3K7m8ihWlNcEGGwygu0AcLwtADAaldmQToM+Yz0P9HgB1xuMex\nmxfw212x9fAMjYhQkVQrzspA/QSB9z2WESfuCs1DYK9bvx+tXjaIg0Hm4ZwbmPvv\nD2RUeOtYlDJeKjmN4Z0XilvIhmllfk/EHkYW4MKWK5K+Nqh04NjMZ8L4yUNZC/M4\nzQDRuh9C/Y45Qld7/KlzavaQz94xv6spsKbvGlzakN6GlCEpbwZCrovL0L7hM9IY\nVeq8ff4uw6RC4bR6IiVWjrhYtwmy/GpGt3fblhcxkQKBgQDpN8uLGpfOw4pfCh9d\nfIaNJ5aa0SqKZRviTaJccFnftPg/+Wk0hQNUOq3TDOSZxgK8hD2D4tNC4Nv9ZO9c\ni+xqBULUFY3CDJWdQqntCWWC6DBokfzDHQr+gDI8+Moocyi2cqjN4cfbMX6/xX3j\nOvRrZ3F9pscXC64Wix7pw7bpRwKBgQC2Bk7HfBUX71pAQith8CaISU+aY+JA+DTf\nibNjZfORpfDH2fSrjh1+M2Nsoi1jsAPnUEAckxbtBAOBOp9w70tmU/riLfdQ1H8F\nj9pDn35ohpP4cYnN2mQqxNY+jL4pI+Qu/SWf63oXvG2AC7X4RSidtI1Q6J3HH+z2\nQu1BNSvQzQKBgEt3cZcOwOb4YZNFfEbNH+EXWWW5n5FvDGnbg1l0RbDdJ6PT+lYz\nYJNl9Y+g0WxtJb+I7zr5MDGo/6bsfYQuBw97qldkrh2H4vYjd0crzjxhFCESdH9S\nq5cVNqyCOTCDqz32tmcA06I7Tu+RYZ4hGqySqafmSvBLKEdFN3ifi1XLAoGAHnZ/\nyLLjdNYB7K8mQ4XFbRmX0ObWfrkLYD3TX9c4JC/5U/kOEYf/N5eyFAQwRHa4sIWl\ntSKIu7HoREBjXqstmzqCykeXFFf7yhqBFMAkj6m2KeYWgfUCvoWitWUojgoLrjF/\nknv+Ouq2CK/tDFfGrF4DKH9FqIWXSr94pWkYpCECgYBWA2Lv2+3uoVROgLuTAJbj\nIhZ0LvK6gBt0OPXMLDEcIXndcaUNSTC9l8+Fl2eNaMcNc/1mKQhfRR1GA17kuXKf\nNqW+rTlxa7mr/0S/DMUaxzccFfoQKr/x7uV1MvJulOaXGxpOXj+MoHVSCNm4lXbi\n6dNL9/RkDlm9am2Ct1RdeA==\n-----END PRIVATE KEY-----\n\", \"client_email\": \"akshaysdemo@appspot.gserviceaccount.com\", \"client_id\": \"117196345125732155646\", \"auth_uri\": \"https://accounts.google.com/o/oauth2/auth\", \"token_uri\": \"https://oauth2.googleapis.com/token\", \"auth_provider_x509_cert_url\": \"https://www.googleapis.com/oauth2/v1/certs\", \"client_x509_cert_url\": \"https://www.googleapis.com/robot/v1/metadata/x509/akshaysdemo%40appspot.gserviceaccount.com\" }";

        public static DbTypeQueries dbTypeQueries = new DbTypeQueries();

        public Startup(IConfiguration configuration)
        {
            AkshaySDemoContext.ConnectionString = AkshaySDemoDBConnStr;
            AkshaySDemoMySQLContext.ConnectionString = MySQLConnStr;
            AkshaySDemoPostgreSQLContext.ConnectionString = PostgreSQLConnStr;
            ChefDataAccessLayer.GoogleFirestoreDBJsonString = GoogleFirestoreDBJsonString;
            dbTypeQueries.ConnectionString = AkshaySDemoDBConnStr;
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
