using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SpotifyApi.NetCore;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EpicAkSBlazorWMusicClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
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
