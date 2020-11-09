using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthenticationPagesForUnity.Pages
{
    public class SpotifyAuthModel : PageModel
    {
        [FromQuery]
        public string unityToken { get; set; } = null;

        [FromQuery]
        public string accessToken { get; set; }

        public string msg { get; set; }

        public void OnGet()
        {
            if (string.IsNullOrWhiteSpace(accessToken) && !string.IsNullOrWhiteSpace(unityToken))
            {
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Append("unityToken", unityToken, cookieOptions);
                string scopes = WebUtility.UrlEncode("user-read-playback-position user-read-email user-library-read user-top-read playlist-modify-public user-follow-read user-read-playback-state user-modify-playback-state user-read-private playlist-read-private user-library-modify playlist-read-collaborative playlist-modify-private user-follow-modify user-read-currently-playing user-read-recently-played");
                string redirectUri = WebUtility.UrlEncode("https://localhost:44325/SpotifyAPI/AccessSpotifyToken");
                Response.Redirect($"https://accounts.spotify.com/authorize?response_type=code&client_id={Globals.SpotifyClientId}&scope={scopes}&redirect_uri={redirectUri}");
            }
            else if (!string.IsNullOrWhiteSpace(accessToken))
            {
                UnityToken unityToken = new UnityToken(Request.Cookies["unityToken"]);
                unityToken.UTSpotifyToken.access_token = accessToken;
                Globals.UnityTokens.Add(unityToken);
                msg = "Successfully logged into Spotify.";
                Response.Redirect("TestShowSpotifyToken");
            }
            else
            {
                msg = "Failed to login to Spotify.";
            }
        }
    }
}
