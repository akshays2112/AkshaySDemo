using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;

namespace EpicAkSAuthenticationPages.Pages
{
    public class IndexModel : PageModel
    {
        [FromQuery]
        public string clientId { get; set; }
        public string clientSecret { get; set; }

        public void OnGet()
        {
            HttpClient httpClient = new HttpClient();
            string clientAppToken = httpClient.GetAsync($"https://localhost:44325/RegisterApiInfo?clientId={clientId ?? (Globals.IS_DEVELOPMENT_ENVIRONMENT ? Globals.MySpotifyClientID : "")}&clientSecret={clientSecret ?? (Globals.IS_DEVELOPMENT_ENVIRONMENT ? Globals.MySpotifyClientSecret : "")}").Result.Content.ReadAsStringAsync().Result;
            Response.Redirect($"/SpotifyAuth?clientAppToken={clientAppToken}");
        }
    }
}
