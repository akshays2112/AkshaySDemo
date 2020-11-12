using Microsoft.AspNetCore.Mvc;
using SpotifyApi.NetCore;
using System.Linq;
using Newtonsoft.Json;
using EpicAkSAuthenticationPages.Models;

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

        [HttpPost]
        public string Post(ClientAppTokenValue clientAppToken)
        {
            ClientAppToken token = Globals.ClientAppTokens.First(ut => ut.CATToken == clientAppToken.clientAppToken);
            return JsonConvert.SerializeObject(new GenericWebSvcReturnObjWrapper(token, token?.CATSpotifyAPIData.SADSpotifyPlaylists));
        }
    }
}
