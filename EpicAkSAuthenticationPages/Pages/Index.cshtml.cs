using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using EpicAkSAuthenticationPages.Models;

namespace EpicAkSAuthenticationPages.Pages
{
    public class IndexModel : PageModel
    {
        [FromQuery]
        public string clientId { get; set; }

        [FromQuery]
        public string clientSecret { get; set; }

        public void OnGet()
        {
            HttpClient httpClient = new HttpClient();
            StringContent content = null;
            content = new StringContent(JsonConvert.SerializeObject(new ApiInfoValues
            {
                clientId = clientId ?? (Globals.IS_DEVELOPMENT_ENVIRONMENT ? Globals.MySpotifyClientID : ""),
                clientSecret = clientSecret ?? (Globals.IS_DEVELOPMENT_ENVIRONMENT ? Globals.MySpotifyClientSecret : "")
            }));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var request = httpClient.PostAsync($"https://localhost:44325/RegisterApiInfo", content).Result;
            var response = request.Content.ReadAsStringAsync().Result;
            string clientAppToken = response;
            Response.Redirect($"/SpotifyAuth?clientAppToken={clientAppToken}");
        }
    }
}
