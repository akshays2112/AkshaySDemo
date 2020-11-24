using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Text;

namespace EpicAkSAuthenticationPages.Pages
{
    public class TestShowSpotifyTokenModel : PageModel
    {
        [FromQuery]
        public string catTempSessionUID { get; set; }

        public ClientAppToken.SpotifyApi.SpotifyPlaylists.SpotifyPlaylist[] spotifyPlaylists { get; set; }

        public void OnGet()
        {
            ClientAppToken clientAppToken = ClientAppToken.Globals.ClientAppTokens.Find(cat => cat.CATTempSessionUID == catTempSessionUID);
            //clientAppToken.CATTempSessionUID = string.Empty;
            spotifyPlaylists = clientAppToken.CATSpotifyApi.SASpotifyPlaylists.Playlists;
        }
    }
}
