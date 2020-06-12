using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SyncMusicFromExternalSources.Areas.Identity;
using SyncMusicFromExternalSources.Data;
using System.Net.Http;
using SpotifyApi.NetCore;
using SpotifyApi.NetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SyncMusicFromExternalSources
{
    public class Startup
    {
        public static string UserAccessToken;
        public const string UserAccountName = "jkdesxdxvu6uetjdnaro2yrfc";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

            services.AddAuthentication().AddSpotify(options => {
                options.ClientId = "d0052cf8055246fa8dbd71b5b84284be";
                options.ClientSecret = "a998f5872f93419fb01f3b30c31cb6e3";
                options.CallbackPath = "/SpotifyAPI/spotifylistplaylists";
                options.SaveTokens = true;

                /*
                string[] items = {
                        "playlist-read-public", "user-read-email"
                    };
                foreach (var item in items)
                {
                    options.Scope.Add(item);
                }
                */

                options.Events.OnRemoteFailure = (context) =>
                {
                    return Task.CompletedTask;
                };

                options.Events.OnCreatingTicket = ctx =>
                {
                    List<AuthenticationToken> tokens = ctx.Properties.GetTokens().ToList();

                    tokens.Add(new AuthenticationToken()
                    {
                        Name = "TicketCreated",
                        Value = DateTime.UtcNow.ToString()
                    });

                    ctx.Properties.StoreTokens(tokens);

                    UserAccessToken = ctx.Properties.GetTokenValue("access_token");

                    return Task.CompletedTask;
                };
            });

            services.AddSingleton<HttpClient>(new HttpClient());
            services.AddSingleton(typeof(IPlaylistsApi), typeof(PlaylistsApi));
            services.AddSingleton(typeof(IArtistsApi), typeof(ArtistsApi));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
