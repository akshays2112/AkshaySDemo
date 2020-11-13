using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

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
            string clientAppToken = Globals.GenerateClientAppToken(clientId, clientSecret).CATToken;
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddMinutes(30);
            Response.Cookies.Append("clientAppToken", clientAppToken, cookieOptions);
            Response.Redirect($"/SpotifyAuth");
        }
    }
}
