using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SpotifyApi.NetCore;

namespace AkshaysSpotifyClient
{
    public class Program
    {
        public static SpotifyAccessToken SpotifyAccessToken;
        public static GoogleApisYoutubeAccessToken GoogleApisYoutubeAccessToken;
        public static string GoogleApisApplicationName = "SyncMusicFromExternalSources";
        public static string GoogleApisApiKey = "AIzaSyD_3_i40itVVogJE2qMyGJ8TKX5C1lwnxw";
        public static string Testing;
        public static int DivIndex = 0;

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton(typeof(IPlaylistsApi), typeof(PlaylistsApi));
            builder.Services.AddSingleton(typeof(IUsersProfileApi), typeof(UsersProfileApi));
            builder.Services.AddSingleton(typeof(IFollowApi), typeof(FollowApi));
            builder.Services.AddSingleton(typeof(ISearchApi), typeof(SearchApi));

            await builder.Build().RunAsync();
        }
    }
}
