using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;

namespace AuthenticationPagesForUnity.Controllers
{
    [ApiController]
    [Route("SpotifyAPI/[controller]")]
    public class AccessSpotifyToken : ControllerBase
    {
        [HttpGet]
        public void Get(string accessToken = null, string unityToken = null)
        {
            if (!string.IsNullOrWhiteSpace(accessToken) && !string.IsNullOrWhiteSpace(unityToken))
            {
                Globals.SpotifyToken.access_token = accessToken;
                Globals.SpotifyToken.unityToken = unityToken;
            }
            else
            {
                string code = Request.Query["code"];
                HttpClient httpClient = new HttpClient();
                if (!string.IsNullOrWhiteSpace(code))
                {
                    string credentials = String.Format("{0}:{1}", Globals.SpotifyClientId, Globals.SpotifyClientSecret);

                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));

                    //Prepare Request Body
                    List<KeyValuePair<string, string>> requestData = new List<KeyValuePair<string, string>>();
                    requestData.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
                    requestData.Add(new KeyValuePair<string, string>("code", code));
                    requestData.Add(new KeyValuePair<string, string>("redirect_uri", "https://localhost:44325/SpotifyAPI/AccessSpotifyToken"));

                    FormUrlEncodedContent requestBody = new FormUrlEncodedContent(requestData);

                    //Request Token
                    var request = httpClient.PostAsync("https://accounts.spotify.com/api/token", requestBody).Result;
                    var response = request.Content.ReadAsStringAsync().Result;
                    Response.Redirect($"/SpotifyAuth?accessToken={JsonConvert.DeserializeObject<SpotifyToken>(response).access_token}");
                }
            }
        }
    }
}
