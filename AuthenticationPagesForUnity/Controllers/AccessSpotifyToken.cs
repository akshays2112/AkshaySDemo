using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AllWebApisForUnity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccessSpotifyTokenController : ControllerBase
    {
        [HttpGet]
        public SpotifyToken Get()
        {
            return Globals.SpotifyToken;
        }

        [HttpPost]
        public void Post(SpotifyToken spotifyToken)
        {
            if (!string.IsNullOrWhiteSpace(spotifyToken.access_token))
            {
                Globals.SpotifyToken = spotifyToken;
            }
        }
    }
}
