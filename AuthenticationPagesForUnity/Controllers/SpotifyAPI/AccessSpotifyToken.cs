using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace AuthenticationPagesForUnity.Controllers
{
    [ApiController]
    [Route("SpotifyAPI/[controller]")]
    public class AccessSpotifyToken : ControllerBase
    {
        [HttpGet]
        public void Get(string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(accessToken)) {
                string code = Request.Query["code"];
                HttpClient httpClient = new HttpClient();
                if (!string.IsNullOrWhiteSpace(code))
                {
                    string credentials = String.Format("{0}:{1}", Globals.SpotifyClientId, Globals.SpotifyClientSecret);

                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));

                    List<KeyValuePair<string, string>> requestData = new List<KeyValuePair<string, string>>();
                    requestData.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
                    requestData.Add(new KeyValuePair<string, string>("code", code));
                    requestData.Add(new KeyValuePair<string, string>("redirect_uri", "https://localhost:44325/SpotifyAPI/AccessSpotifyToken"));

                    FormUrlEncodedContent requestBody = new FormUrlEncodedContent(requestData);

                    Response.Redirect($"/SpotifyAuth?accessToken={JsonConvert.DeserializeObject<UnityToken.SpotifyToken>(httpClient.PostAsync("https://accounts.spotify.com/api/token", requestBody).Result.Content.ReadAsStringAsync().Result).access_token}");
                }
            }
        }
    }
}
