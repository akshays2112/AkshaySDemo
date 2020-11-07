using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using AllWebApisForUnity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AuthenticationPagesForUnity.Pages
{
    public class SpotifyAuthModel : PageModel
    {
        [FromQuery]
        public string code { get; set; }

        public string access_token { get; set; }

        public void OnGet()
        {
            string SpotifyClientId = "d0052cf8055246fa8dbd71b5b84284be";
            HttpClient httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:44325/SpotifyAuth") };
            if (string.IsNullOrWhiteSpace(code))
            {
                string scopes = WebUtility.UrlEncode("user-read-playback-position user-read-email user-library-read user-top-read playlist-modify-public user-follow-read user-read-playback-state user-modify-playback-state user-read-private playlist-read-private user-library-modify playlist-read-collaborative playlist-modify-private user-follow-modify user-read-currently-playing user-read-recently-played");
                string redirectUri = WebUtility.UrlEncode(httpClient.BaseAddress.ToString());
                Response.Redirect($"https://accounts.spotify.com/authorize?response_type=code&client_id={SpotifyClientId}&scope={scopes}&redirect_uri={redirectUri}");
            }
            else if (!string.IsNullOrWhiteSpace(code))
            {
                string SpotifyClientSecret = "a998f5872f93419fb01f3b30c31cb6e3";
                string credentials = String.Format("{0}:{1}", SpotifyClientId, SpotifyClientSecret);

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));

                //Prepare Request Body
                List<KeyValuePair<string, string>> requestData = new List<KeyValuePair<string, string>>();
                requestData.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
                requestData.Add(new KeyValuePair<string, string>("code", code));
                requestData.Add(new KeyValuePair<string, string>("redirect_uri", httpClient.BaseAddress.ToString()));

                FormUrlEncodedContent requestBody = new FormUrlEncodedContent(requestData);

                //Request Token
                var request = httpClient.PostAsync("https://accounts.spotify.com/api/token", requestBody).Result;
                var response = request.Content.ReadAsStringAsync().Result;
                Globals.SpotifyToken = JsonConvert.DeserializeObject<SpotifyToken>(response);
                access_token = Globals.SpotifyToken.access_token;
            }
        }
    }
}
