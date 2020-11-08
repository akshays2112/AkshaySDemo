using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthenticationPagesForUnity.Pages
{
    public class SpotifyAuthModel : PageModel
    {
        [FromQuery]
        public string unityToken { get; set; }

        [FromQuery]
        public string accessToken { get; set; }

        public string spotifyAuthorizeRedirectURI { get; set; }

        public void OnGet()
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                string scopes = WebUtility.UrlEncode("user-read-playback-position user-read-email user-library-read user-top-read playlist-modify-public user-follow-read user-read-playback-state user-modify-playback-state user-read-private playlist-read-private user-library-modify playlist-read-collaborative playlist-modify-private user-follow-modify user-read-currently-playing user-read-recently-played");
                string redirectUri = WebUtility.UrlEncode("https://localhost:44325/SpotifyAPI/AccessSpotifyToken");
                spotifyAuthorizeRedirectURI = $"https://accounts.spotify.com/authorize?response_type=code&client_id={Globals.SpotifyClientId}&scope={scopes}&redirect_uri={redirectUri}";
            }
        }
    }
}
