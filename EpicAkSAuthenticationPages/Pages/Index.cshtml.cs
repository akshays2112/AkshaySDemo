using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using EpicAkSAuthenticationPages.Models;
using System;
using Microsoft.AspNetCore.Http;

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
