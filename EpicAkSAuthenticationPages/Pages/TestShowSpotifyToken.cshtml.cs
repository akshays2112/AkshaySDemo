using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using EpicAkSAuthenticationPages.Models;
using System;

namespace EpicAkSAuthenticationPages.Pages
{
    public class TestShowSpotifyTokenModel : PageModel
    {
        public ClientAppToken catToken { get; set; }

        public void OnGet()
        {
            catToken = Globals.ClientAppTokens.Find(cat => cat.CATToken == Request.Cookies["clientAppToken"]);
        }
    }
}
