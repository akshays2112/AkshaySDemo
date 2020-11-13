using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EpicAkSAuthenticationPages.Pages
{
    public class TestShowSpotifyTokenModel : PageModel
    {
        public ClientAppToken catToken { get; set; }

        public void OnGet()
        {
            catToken = Globals.ClientAppTokens.Find(cat => cat.CATToken == Request.Cookies["clientAppToken"]);
            _ = Globals.ClientAppTokens;
        }
    }
}
