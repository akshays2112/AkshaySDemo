using System;

namespace AllWebApisForUnity
{
    public class SpotifyToken
    {
        public string access_token { get; set; }

        public SpotifyToken(string accessToken)
        {
            access_token = accessToken;
        }
    }

    public class Globals
    {
        public static SpotifyToken SpotifyToken { get; set; } = new SpotifyToken("Need to login to Spotify to get the Token.");
    }
}
