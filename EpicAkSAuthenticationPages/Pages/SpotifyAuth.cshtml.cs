using EpicAkSAuthenticationPages.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace EpicAkSAuthenticationPages.Pages
{
    public class SpotifyAuthModel : PageModel
    {
        [FromQuery]
        public string clientAppToken { get; set; } = null;

        [FromQuery]
        public string code { get; set; } = null;

        public string msg { get; set; }

        public void OnGet()
        {
            if (!string.IsNullOrWhiteSpace(clientAppToken) && string.IsNullOrWhiteSpace(code))
            {
                ClientAppToken token = Globals.ClientAppTokens.Find(ut => ut.CATToken == clientAppToken);
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Append("clientAppToken", clientAppToken, cookieOptions);
                string scopes = WebUtility.UrlEncode("user-read-playback-position user-read-email user-library-read user-top-read playlist-modify-public user-follow-read user-read-playback-state user-modify-playback-state user-read-private playlist-read-private user-library-modify playlist-read-collaborative playlist-modify-private user-follow-modify user-read-currently-playing user-read-recently-played");
                string redirectUri = WebUtility.UrlEncode($"{Globals.BaseRedirectUri}/SpotifyAuth");
                Response.Redirect($"https://accounts.spotify.com/authorize?response_type=code&client_id={token.CATSpotifyAPIInfo.ClientId}&scope={scopes}&redirect_uri={redirectUri}");
            }
            else if(!string.IsNullOrWhiteSpace(code))
            {
                clientAppToken = Request.Cookies["clientAppToken"];
                ClientAppToken token = Globals.ClientAppTokens.Find(ut => ut.CATToken == clientAppToken);
                HttpClient httpClient = new HttpClient { BaseAddress = new Uri(Globals.BaseRedirectUri) };
                string credentials = String.Format("{0}:{1}", token.CATSpotifyAPIInfo.ClientId, token.CATSpotifyAPIInfo.ClientSecret);

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));

                //Prepare Request Body
                List<KeyValuePair<string, string>> requestData = new List<KeyValuePair<string, string>>();
                requestData.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
                requestData.Add(new KeyValuePair<string, string>("code", code));
                requestData.Add(new KeyValuePair<string, string>("redirect_uri", $"{Globals.BaseRedirectUri}/SpotifyAuth"));

                FormUrlEncodedContent requestBody = new FormUrlEncodedContent(requestData);

                //Request Token
                HttpResponseMessage request = httpClient.PostAsync("https://accounts.spotify.com/api/token", requestBody).Result;
                string response = request.Content.ReadAsStringAsync().Result;
                token.CATSpotifyToken  = JsonConvert.DeserializeObject<ClientAppToken.SpotifyToken>(response);
                msg = "Successfully logged into Spotify.";
                Response.Redirect($"/TestShowSpotifyToken?clientAppToken={clientAppToken}");
            }
            else
            {
                msg = "Failed to login to Spotify.";
            }
        }
    }
}
