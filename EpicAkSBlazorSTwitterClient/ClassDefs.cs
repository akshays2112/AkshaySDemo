using Newtonsoft.Json;
using Tweetinvi.Auth;
using Tweetinvi.Models;
using Tweetinvi.Credentials.Models;

namespace EpicAkSBlazorSTwitterClient
{
    public class Globals
    {
        public static int DivIndex = 0;
        public static string RedirectUri;
        public static string TwitterApiKey = "ngtvtlOEDXogCOvgxskFAZckI";
        public static string TwitterApiSecret = "6szb2CphSAK4wMXm8fW8UO8UnEY3Ql0WEANihtCvDt2GJVeJfb";
        public static string TwitterAccessToken = string.Empty;
        public static LocalAuthenticationRequestStore _myAuthRequestStore = new LocalAuthenticationRequestStore();
        public static ITwitterCredentials UserCredentials;

        public class JSTempAuthRequestStoreHolder
        {
            public string authenticationRequestId { get; set; }
            public string redirectPath { get; set; }
            public AuthenticationRequest authenticationRequestToken { get; set; }
        }
    }

    public class TwitterAccessToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }

    public class MyTweetsList
    {
        [JsonProperty("data")]
        public MyTweetInfo[] Data { get; set; }
    }

    public class MyTweetInfo
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
