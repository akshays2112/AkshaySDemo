using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EpicAkSAuthenticationPages.Pages
{
    public class SpotifyAuthModel : PageModel
    {
        [FromQuery]
        public string unityToken { get; set; } = null;

        [FromQuery]
        public string spotifyClientId { get; set; } = null;

        [FromQuery]
        public string spotifyClientSecret { get; set; } = null;

        [FromQuery]
        public string code { get; set; } = null;

        public string msg { get; set; }

        public void OnGet()
        {
            if(Globals.IS_DEVELOPMENT_ENVIRONMENT && string.IsNullOrWhiteSpace(spotifyClientId) && string.IsNullOrWhiteSpace(spotifyClientSecret))
            {
                spotifyClientId = Globals.MySpotifyClientID;
                spotifyClientSecret = Globals.MySpotifyClientSecret;
            }
            if (!string.IsNullOrWhiteSpace(unityToken) && !string.IsNullOrWhiteSpace(spotifyClientId) && !string.IsNullOrWhiteSpace(spotifyClientSecret))
            {
                UnityToken token = new UnityToken(unityToken);
                token.UTSpotifyAPIInfo.ClientId = spotifyClientId;
                token.UTSpotifyAPIInfo.ClientSecret = spotifyClientSecret;
                Globals.UnityTokens.Add(token);
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Append("unityToken", unityToken, cookieOptions);
                string scopes = WebUtility.UrlEncode("user-read-playback-position user-read-email user-library-read user-top-read playlist-modify-public user-follow-read user-read-playback-state user-modify-playback-state user-read-private playlist-read-private user-library-modify playlist-read-collaborative playlist-modify-private user-follow-modify user-read-currently-playing user-read-recently-played");
                string redirectUri = WebUtility.UrlEncode("https://localhost:44325/SpotifyAuth");
                Response.Redirect($"https://accounts.spotify.com/authorize?response_type=code&client_id={token.UTSpotifyAPIInfo.ClientId}&scope={scopes}&redirect_uri={redirectUri}");
            }
            else if(!string.IsNullOrWhiteSpace(code))
            {
                unityToken = Request.Cookies["unityToken"];
                UnityToken token = Globals.UnityTokens.Find(ut => ut.UTUnityToken == unityToken);
                HttpClient httpClient = new HttpClient();
                string credentials = String.Format("{0}:{1}", token.UTSpotifyAPIInfo.ClientId, token.UTSpotifyAPIInfo.ClientSecret);

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));

                List<KeyValuePair<string, string>> requestData = new List<KeyValuePair<string, string>>();
                requestData.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
                requestData.Add(new KeyValuePair<string, string>("code", code));
                requestData.Add(new KeyValuePair<string, string>("redirect_uri", "https://localhost:44325/SpotifyAuth"));

                FormUrlEncodedContent requestBody = new FormUrlEncodedContent(requestData);
                string response = httpClient.PostAsync("https://accounts.spotify.com/api/token", requestBody).Result.Content.ReadAsStringAsync().Result;
                token.UTSpotifyToken.access_token  = JsonConvert.DeserializeObject<UnityToken.SpotifyToken>(response).access_token;
                msg = "Successfully logged into Spotify.";
                Response.Redirect($"/TestShowSpotifyToken?unityToken={unityToken}");
            }
            else
            {
                msg = "Failed to login to Spotify.";
            }
        }
    }
}
