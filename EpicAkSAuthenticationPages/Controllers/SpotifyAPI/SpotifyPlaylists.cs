using Microsoft.AspNetCore.Mvc;
using SpotifyApi.NetCore;
using System.Linq;
using Newtonsoft.Json;

namespace EpicAkSAuthenticationPages.Controllers.SpotifyAPI
{
    [Route("SpotifyAPI/[controller]")]
    [ApiController]
    public class SpotifyPlaylists : ControllerBase
    {
        public SpotifyPlaylists(IPlaylistsApi playlistsApi, IUsersProfileApi usersProfileApi)
        {
            Globals.PlaylistsApi ??= playlistsApi;
            Globals.UsersProfileApi ??= usersProfileApi;
        }

        [HttpGet]
        public string Get(string clientAppToken)
        {
            ClientAppToken token = Globals.ClientAppTokens.First(ut => ut.CATToken == clientAppToken);
            return JsonConvert.SerializeObject(token?.CATSpotifyAPIData.SADSpotifyPlaylists);
        }
    }
}
