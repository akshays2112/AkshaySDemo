using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;

namespace EpicAkSAuthenticationPages
{
    public class ClientAppToken
    {
        public class ApiInfo
        {
            public string ClientId { get; set; }
            public string ClientSecret { get; set; }
        }

        public class SpotifyToken
        {
            public string access_token { get; set; }

            public SpotifyToken(string accessToken)
            {
                access_token = accessToken;
            }
        }

        public class SpotifyAPI
        {
            public class SpotifyAPIJsonUserProfile
            {
                public string id { get; set; }
            }

            public class SpotifyAPIJsonPlaylist
            {
                public string id { get; set; }
                public string name { get; set; }
            }

            public class SpotifyAPIJsonPlaylists
            {
                public SpotifyAPIJsonPlaylist[] items { get; set; }
            }

            public class UserProfile
            {
                public ClientAppToken SAD_UP_ClientAppToken { get; set; }

                private string id;
                public string ID { 
                    get 
                    {
                        if(string.IsNullOrWhiteSpace(id))
                        {
                            Init();
                        }
                        return id;
                    }
                }

                public UserProfile(ClientAppToken clientAppToken)
                {
                    SAD_UP_ClientAppToken = clientAppToken;
                }

                private void Init()
                {
                    string accessToken = SAD_UP_ClientAppToken?.CATSpotifyToken?.access_token;
                    if (!string.IsNullOrWhiteSpace(accessToken))
                    {
                        HttpClient httpClient = new HttpClient();
                        httpClient.DefaultRequestHeaders.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", 
                            SAD_UP_ClientAppToken.CATSpotifyToken.access_token);
                        id = JsonConvert.DeserializeObject<SpotifyAPIJsonUserProfile>(httpClient.GetAsync("https://api.spotify.com/v1/me").Result.Content.ReadAsStringAsync().Result).id;
                    }
                }
            }

            public class SpotifyPlaylist
            {
                public string Name { get; set; }

                public SpotifyPlaylist(string name)
                {
                    Name = name;
                }
            }

            public class SpotifyPlaylists
            {
                [JsonIgnore]
                public ClientAppToken SAD_PLs_ClientAppToken { get; set; }

                private List<SpotifyPlaylist> playlists;
                public List<SpotifyPlaylist> JsonPlaylists {
                    get
                    {
                        return playlists;
                    }
                    set
                    { 
                        if(value != playlists)
                        {
                            playlists = value;
                        }
                    }
                }
                [JsonIgnore]
                public List<SpotifyPlaylist> Playlists
                {
                    get
                    {
                        if (playlists == null)
                        {
                            Init();
                        }
                        return playlists;
                    }
                }

                public SpotifyPlaylists(ClientAppToken clientAppToken)
                {
                    SAD_PLs_ClientAppToken = clientAppToken;
                }

                public void Init()
                {
                    playlists = new List<SpotifyPlaylist>();
                    HttpClient httpClient = GetSpotifyAPIHttpClient(SAD_PLs_ClientAppToken.CATSpotifyToken.access_token);
                    SpotifyAPIJsonPlaylists spotifyAPIJsonPlaylists = JsonConvert.DeserializeObject<SpotifyAPIJsonPlaylists>(
                        httpClient.GetAsync("https://api.spotify.com/v1/me/playlists").Result.Content.ReadAsStringAsync().Result);
                    foreach (SpotifyAPIJsonPlaylist spotifyAPIJsonPlaylist in spotifyAPIJsonPlaylists.items)
                    {
                        playlists.Add(new SpotifyPlaylist(spotifyAPIJsonPlaylist.name));
                    }
                }
            }

            public ClientAppToken ClientAppToken { get; set; }

            public UserProfile SADUserProfile { get; set; }

            public SpotifyPlaylists SADSpotifyPlaylists { get; set; }

            public SpotifyAPI(ClientAppToken clientAppToken)
            {
                ClientAppToken = clientAppToken;
                SADUserProfile = new UserProfile(clientAppToken);
                SADSpotifyPlaylists = new SpotifyPlaylists(clientAppToken);
            }

            protected static HttpClient GetSpotifyAPIHttpClient(string accessToken)
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                return httpClient;
            }
        }

        public string CATToken { get; set; }
        public SpotifyToken CATSpotifyToken { get; set; }
        public ApiInfo CATSpotifyAPIInfo { get; set; }
        public SpotifyAPI CATSpotifyAPI { get; set; }

        public ClientAppToken()
        {
            CATSpotifyAPI = new SpotifyAPI(this);
        }

        public ClientAppToken(string clientAppToken)
        {
            CATToken = clientAppToken;
            CATSpotifyAPI = new SpotifyAPI(this);
        }
    }

    public static class Globals
    {
        public static string BaseRedirectUri;

        public const string MySpotifyClientID = "d0052cf8055246fa8dbd71b5b84284be";
        public const string MySpotifyClientSecret = "a998f5872f93419fb01f3b30c31cb6e3";

        public static List<ClientAppToken> ClientAppTokens = new List<ClientAppToken>();

        public static ClientAppToken GenerateClientAppToken(string clientId, string clientSecret)
        {
            ClientAppToken caToken = new ClientAppToken
            {
                CATToken = GenerateRandomClientAppToken(),
                CATSpotifyAPIInfo = new ClientAppToken.ApiInfo
                {
                    ClientId = clientId ?? MySpotifyClientID,
                    ClientSecret = clientSecret ?? MySpotifyClientSecret
                }
            };
            ClientAppTokens.Add(caToken);
            return caToken;
        }

        public static string GenerateRandomClientAppToken()
        {
            const string numbers = "54882046652138414695194151160943305727036575959195309218611738193261179310511854807446237996274956735188575272489122793818301194912983367336244065664308602139494639522473719070217986094370277053921717629317675238467481846766940513200056812714526356082778577134275778960917363717872146844090122495343014654958537105079227968925892354201995611212902196086403441815981362977477130996051870721134999999837297804995105973173281609631859502445945534690830264252230825334468503526193118817101000313783875288658753320838142061717766914730359825349042875546873115956286388235378759375195778185778053217122680661300192787661119590921642019893809525720106548586327886593615338182796823030195203530185296899577362259941389124972177528347913151557485724245415069595082953311686172785588907509838175463746493931925506040092770167113900984882401285836160356370766010471018194295559619894676783744944825537977472684710404753464620804668425906949129331367702898915210475216205696602405803815019351125338243003558764024749647326391419927260426992279678235478163600934172164121992458631503028618297455570674983850549458858692699569092721079750930295532116534498720275596023648066549911988183479775356636980742654252786255181841757467289097777279380008164706001614524919217321721477235014144197356854816136115735255213347574184946843852332390739414333454776241686251898356948556209921922218427255025425688767179049460165346680498862";
            StringBuilder randomClientAppToken = new StringBuilder();
            Random random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < 100; i++)
            {
                int numbersIndex = random.Next(numbers.Length - 1);
                char numChar = numbers[numbersIndex];
                randomClientAppToken.Append(numChar);
            }
            return randomClientAppToken.ToString();
        }
    }

    public class GenericWebSvcReturnObjWrapper
    {
        public string ClientAppToken { get; set; }
        public string ObjectType { get; set; }
        public string JsonObject { get; set; }

        [JsonConstructor]
        public GenericWebSvcReturnObjWrapper() { }

        public GenericWebSvcReturnObjWrapper(ClientAppToken clientAppToken, object obj)
        {
            clientAppToken.CATToken = Globals.GenerateRandomClientAppToken();
            ClientAppToken = clientAppToken.CATToken;
            ObjectType = obj.GetType().FullName;
            JsonObject = JsonConvert.SerializeObject(obj);
        }
    }
}
