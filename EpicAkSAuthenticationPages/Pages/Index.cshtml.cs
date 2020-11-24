using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;

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
            string catTempSessionUID = ClientAppToken.SpotifyApi.GenerateClientAppTokenForSpotifyApi(clientId, clientSecret).CATTempSessionUID;
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddMinutes(30);
            Response.Cookies.Append("catTempSessionUID", catTempSessionUID, cookieOptions);
            Response.Redirect($"/SpotifyAuth");
        }
    }
}
