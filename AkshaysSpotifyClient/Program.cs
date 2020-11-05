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
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddSingleton(new HttpClient());
            builder.Services.AddSingleton(typeof(IPlaylistsApi), typeof(PlaylistsApi));
            builder.Services.AddSingleton(typeof(IArtistsApi), typeof(ArtistsApi));
            builder.Services.AddSingleton(typeof(IUsersProfileApi), typeof(UsersProfileApi));
            builder.Services.AddSingleton(typeof(IFollowApi), typeof(FollowApi));
            builder.Services.AddSingleton(typeof(ISearchApi), typeof(SearchApi));
            builder.Services.AddSingleton(typeof(IPlayerApi), typeof(PlayerApi));

            await builder.Build().RunAsync();
        }
    }
}
