using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpotifyApi.NetCore;
using System;
using System.Net.Http;

namespace EpicAkSBlazorSMusicClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Globals.RedirectUri = configuration.GetValue<string>("RedirectUri");
            Globals.GoogleApisYoutubeClientId = configuration.GetValue<string>("GoogleApisYoutubeClientId");
            Globals.SpotifyClientId = configuration.GetValue<string>("SpotifyClientId");
            Globals.SpotifyClientSecret = configuration.GetValue<string>("SpotifyClientSecret");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddSingleton(new HttpClient { BaseAddress = new Uri(Globals.RedirectUri) });
            services.AddSingleton(typeof(IPlaylistsApi), typeof(PlaylistsApi));
            services.AddSingleton(typeof(IArtistsApi), typeof(ArtistsApi));
            services.AddSingleton(typeof(IUsersProfileApi), typeof(UsersProfileApi));
            services.AddSingleton(typeof(IFollowApi), typeof(FollowApi));
            services.AddSingleton(typeof(ISearchApi), typeof(SearchApi));
            services.AddSingleton(typeof(IPlayerApi), typeof(PlayerApi));
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
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
