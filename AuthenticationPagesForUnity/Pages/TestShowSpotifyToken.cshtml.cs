using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationPagesForUnity.Pages
{
    public class TestShowSpotifyTokenModel : PageModel
    {
        [FromQuery]
        public string unityToken { get; set; }

        public UnityToken.SpotifyAPIData.SpotifyPlaylists MySpotifyPlaylists { get; set; }

        public void OnGet()
        {
            HttpClient httpClient = new HttpClient();
            var request = httpClient.GetAsync($"https://localhost:44325/SpotifyAPI/SpotifyPlaylists?unityToken={unityToken}").Result;
            var response = request.Content.ReadAsStringAsync().Result;
            MySpotifyPlaylists = JsonConvert.DeserializeObject<UnityToken.SpotifyAPIData.SpotifyPlaylists>(response);
        }
    }
}
