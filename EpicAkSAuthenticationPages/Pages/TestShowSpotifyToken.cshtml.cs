using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace EpicAkSAuthenticationPages.Pages
{
    public class TestShowSpotifyTokenModel : PageModel
    {
        [FromQuery]
        public string clientAppToken { get; set; }

        public ClientAppToken.SpotifyAPIData.SpotifyPlaylists MySpotifyPlaylists { get; set; }

        public void OnGet()
        {
            HttpClient httpClient = new HttpClient();
            var request = httpClient.GetAsync($"https://localhost:44325/SpotifyAPI/SpotifyPlaylists?clientAppToken={clientAppToken}").Result;
            var response = request.Content.ReadAsStringAsync().Result;
            MySpotifyPlaylists = JsonConvert.DeserializeObject<ClientAppToken.SpotifyAPIData.SpotifyPlaylists>(response);
        }
    }
}
