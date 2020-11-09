using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using Newtonsoft.Json;

namespace AuthenticationPagesForUnity.Pages
{
    public class TestShowSpotifyTokenModel : PageModel
    {
        public UnityToken.SpotifyAPIData.SpotifyPlaylists MySpotifyPlaylists { get; set; }

        public void OnGet()
        {
            HttpClient httpClient = new HttpClient();
            var request = httpClient.GetAsync($"https://localhost:44325/SpotifyAPI/SpotifyPlaylists?unityToken=MyUnityToken12345").Result;
            var response = request.Content.ReadAsStringAsync().Result;
            MySpotifyPlaylists = JsonConvert.DeserializeObject<UnityToken.SpotifyAPIData.SpotifyPlaylists>(response);
        }
    }
}
