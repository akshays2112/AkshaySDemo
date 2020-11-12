using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using EpicAkSAuthenticationPages.Models;

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
            StringContent content = null;
            content = new StringContent(JsonConvert.SerializeObject(new ClientAppTokenValue { clientAppToken = clientAppToken }));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var request = httpClient.PostAsync($"https://localhost:44325/SpotifyAPI/SpotifyPlaylists", content).Result;
            var response = request.Content.ReadAsStringAsync().Result;
            GenericWebSvcReturnObjWrapper genericWebSvcObj = JsonConvert.DeserializeObject<GenericWebSvcReturnObjWrapper>(response);
            MySpotifyPlaylists = JsonConvert.DeserializeObject<ClientAppToken.SpotifyAPIData.SpotifyPlaylists>(genericWebSvcObj.JsonObject);
        }
    }
}
