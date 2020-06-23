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
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authentication.Google;
using SyncMusicFromExternalSources.Shared;

namespace SyncMusicFromExternalSources
{
    public class Startup
    {
        public static string SpotifyUserAccessToken;
        public static string GoogleUserAccessToken;
        public static string TwitterUserAccessToken;
        public static string FacebookUserAccessToken;
        public static string GoogleApisOAuthJsonString = "{ \"type\": \"service_account\", \"project_id\": \"syncmusicfromexternalsources\", \"private_key_id\": \"251c4149920fefc41df167de9f7512b9d8b7eea8\", \"private_key\": \"-----BEGIN PRIVATE KEY-----\nMIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQC7ysIMkKi0yEG9\ni+r6WrA9ohJCaQPCoBaoSw527j5f816xCz9225nh7sl+LmzIhD4/fDhqAN4auB5K\nx6cfHb5wN72LbbIL3CRUTOqdY/YAIZNyvJA0UVnF5sYL1mmy3a6LIYcZs5inEELO\nSeStukqFVrO1v68ADsM1iQ+AmBc3PRENSnHxxhIXI2AjalApgq2F2Xp96y/cfaTo\nUka/6uKoJ7of1+Rqlk409RbmzmMnZMZNrx8MueAZcwFAszdny2fVyZh2aFahABcE\n8L8rbktASB1VeBTdQrnRB+t4kt0R90BIsAJe4WLR4HZBRQCGuOAEBdr793Csz1aF\n02O5mIXjAgMBAAECggEAMU44CsKEeQrHL/QnmtnqwhPmhWc7JIORz1f9kT5WRC0K\n5KMWY6eezJI8dxPLXg5SpaqAyVw29H74/RtUhrfxg1cvPgdsIu8G3tdfFjnjof8u\nzoYxunrNfkdsIjmGgP7xT/hal2XvI5YCZFVEZFSwMjgkcQjF8tbtbAXzewjzt32u\n28VKHSnwEo6EJ3b0jf3qUt4pjm1HlFeX/yOCQIpTsW4z3lXDLTpCFRb3LvYBYKmp\npMryi59bktwRe/HuIa8aBgynZXTA9mXPwwD2+3LcSdNCnksfLqNsggRHfem2qS7I\ndQ/DNkJ/G6itfG9UgYOA8UPb13Qp6na25Mjq+9XQ2QKBgQDka43YLGG4T/oGTKqC\nbvsN5guFRpb8f1cfaqtqkoDPszr7PZvxLIj9OuMEK195Ee9bGhdPo88fQ4Yr9hyM\nQOxnbC+E92bvY3/IarR+2Rk/8bzG4QOCssWA6cTp3lm+islnR2j6/9XBhfcwk1VU\n4+qKx8XzgZO5ORRkPiUOQy75bQKBgQDSd2SGz2dlqGpe4fciJTIWTn2oJBwB8iXR\nMT9Zr0XbvATuqay+uXDff+ybbeNXizMtx53zG5WXMJhO20hWJYdU9cTm/tE5IdE1\nnFogNZ7izLLgPl0w6guClfz8AW0q7DTrgA/IsPsnryYAK1fBZ4DjTj1byXqnCF0X\nv9nHsmq6jwKBgHH3w/cW9c7bgJlCCVU5AGRH4nml5dGny5VuSIbNAB4hhA01CSiF\nGviWOAytg15J944R742Z4s7rhvdKPaYxAoL7tJ34PtC0aV5a9yHPOkzwVUs5m+HL\nRsXyXYB+o3JxltnEBHovybxcFiCTcmD/std5o6fCgC1LcIQviZebJ8XlAoGAUo8b\nE/bP7ITAFTA2rWJB35YynH5triW6x/C1lQy4LpX2qVhNtxMvP70Cp4XXpEOqc87W\nGsM7Tu91D5ndKk3WZHF0J9jVYb/uPUVz5SajiPwbPvO3wrRMh5EmiARXOnsl1yuJ\nBYTHQK3oDjDyX5+mY4QRdav3PQZ+3g4X88n9K+kCgYEA4KPTr4yxDnxgryiW3llZ\nOYF2EUZg7JBrDI2qDc1/MzQ3RcVpt1792WZzSGMYTU50VHnjZhAMKazEDfaU2OK0\nN/NrZYtiDutYD0iggSYX7iXRq7zbwS2QFaIjNn0ZUpCdjpEXnkiA0C2eVsS9m90c\nz6s83vXJRioOa+F8ca3NQlk=\n-----END PRIVATE KEY-----\n\", \"client_email\": \"akshaysrin@syncmusicfromexternalsources.iam.gserviceaccount.com\", \"client_id\": \"101764727163111490893\", \"auth_uri\": \"https://accounts.google.com/o/oauth2/auth\", \"token_uri\": \"https://oauth2.googleapis.com/token\", \"auth_provider_x509_cert_url\": \"https://www.googleapis.com/oauth2/v1/certs\", \"client_x509_cert_url\": \"https://www.googleapis.com/robot/v1/metadata/x509/akshaysrin%40syncmusicfromexternalsources.iam.gserviceaccount.com\" }";
        //App Password for SyncMusicFromExternalSources: froeoedbjrvzirxl

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

            services.AddAuthentication().AddSpotify(options =>
            {
                options.ClientId = "d0052cf8055246fa8dbd71b5b84284be";
                options.ClientSecret = "a998f5872f93419fb01f3b30c31cb6e3";
                options.CallbackPath = "/SpotifyAPI/spotifylistplaylists";
                options.SaveTokens = true;

                options.Scope.Add("user-follow-read");
                
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

                    SpotifyUserAccessToken = ctx.Properties.GetTokenValue("access_token");
                    NavMenu.IsLoggedIntoSpotify = true;

                    return Task.CompletedTask;
                };
            }).AddGoogle(options =>
            {
                options.ClientId = "273525569729-e0mbv841egs4unbqfghv10h8om7f4kh3.apps.googleusercontent.com";
                options.ClientSecret = "0EjpmEWT_e8LouksJe0n0MkQ";
                options.CallbackPath = "/YoutubeAPI/youtubelistplaylists";
                options.SaveTokens = true;

                options.Scope.Add("https://www.googleapis.com/auth/youtube.readonly");

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

                    GoogleUserAccessToken = ctx.Properties.GetTokenValue("access_token");
                    NavMenu.IsLoggedIntoGoogle = true;

                    return Task.CompletedTask;
                };
            }).AddTwitter(options =>
            {
                options.ConsumerKey = "ngtvtlOEDXogCOvgxskFAZckI";
                options.ConsumerSecret = "6szb2CphSAK4wMXm8fW8UO8UnEY3Ql0WEANihtCvDt2GJVeJfb";
                options.CallbackPath = "/TwitterAPI/twittertweetslist";
                options.SaveTokens = true;

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

                    TwitterUserAccessToken = ctx.Properties.GetTokenValue("access_token");
                    NavMenu.IsLoggedIntoTwitter = true;

                    return Task.CompletedTask;
                };
            }).AddFacebook(options =>
            {
                options.AppId = "648099945780358";
                options.AppSecret = "e475c46a9298e9b3f72ee7c012edc330";
                options.CallbackPath = "/FacebookAPI/facebookpostslist";
                options.SaveTokens = true;

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

                    FacebookUserAccessToken = ctx.Properties.GetTokenValue("access_token");
                    NavMenu.IsLoggedIntoFacebook = true;

                    return Task.CompletedTask;
                };
            });

            services.AddSingleton<HttpClient>(new HttpClient());
            services.AddSingleton(typeof(IPlaylistsApi), typeof(PlaylistsApi));
            services.AddSingleton(typeof(IArtistsApi), typeof(ArtistsApi));
            services.AddSingleton(typeof(IUsersProfileApi), typeof(UsersProfileApi));
            services.AddSingleton(typeof(IFollowApi), typeof(FollowApi));
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
