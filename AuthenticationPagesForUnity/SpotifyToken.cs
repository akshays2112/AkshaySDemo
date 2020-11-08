using Newtonsoft.Json;

namespace AuthenticationPagesForUnity
{
    public class SpotifyToken
    {
        public string access_token { get; set; }
        public string unityToken { get; set; }

        [JsonConstructor]
        public SpotifyToken(string accessToken)
        {
            access_token = accessToken;
        }

        public SpotifyToken(string accessToken, string unityToken)
        {
            access_token = accessToken;
            this.unityToken = unityToken;
        }
    }

    public class Globals
    {
        public static string SpotifyClientId = "d0052cf8055246fa8dbd71b5b84284be";
        public static string SpotifyClientSecret = "a998f5872f93419fb01f3b30c31cb6e3";

        public static SpotifyToken SpotifyToken { get; set; } = new SpotifyToken("Need to login to Spotify to get the Token.");
    }
}
