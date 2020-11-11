using SpotifyApi.NetCore;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EpicAkSAuthenticationPages
{
    public class ClientAppToken
    {
        public class ApiInfo
        {
            public string ClientId { get; set; }
            public string ClientSecret { get; set; }

            public ApiInfo(string clientId, string clientSecret)
            {
                ClientId = clientId;
                ClientSecret = clientSecret;
            }
        }

        public class SpotifyToken
        {
            public string access_token { get; set; }

            public SpotifyToken(string accessToken)
            {
                access_token = accessToken;
            }
        }

        public class SpotifyAPIData
        {
            public class UserProfile
            {
                public ClientAppToken ClientAppToken { get; set; }

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
                    ClientAppToken = clientAppToken;
                }

                private void Init()
                {
                    string accessToken = ClientAppToken?.CATSpotifyToken?.access_token;
                    if (!string.IsNullOrWhiteSpace(accessToken))
                    {
                        id = Globals.UsersProfileApi.GetCurrentUsersProfile(accessToken).Result.Id;
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
                public ClientAppToken ClientAppToken { get; set; }

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
                    ClientAppToken = clientAppToken;
                }

                public void Init()
                {
                    string accessToken = ClientAppToken?.CATSpotifyToken?.access_token;
                    string userID = ClientAppToken?.CATSpotifyAPIData?.SADUserProfile?.ID;
                    playlists = new List<SpotifyPlaylist>();
                    if (!string.IsNullOrWhiteSpace(accessToken) && !string.IsNullOrWhiteSpace(userID))
                    {
                        PlaylistsSearchResult playlistsSearchResult = Globals.PlaylistsApi.GetPlaylists(userID, accessToken: accessToken).Result;
                        foreach (PlaylistSimplified playlistSimplified in playlistsSearchResult.Items)
                        {
                            playlists.Add(new SpotifyPlaylist(playlistSimplified.Name));
                        }
                    }
                }
            }

            public ClientAppToken ClientAppToken { get; set; }

            public UserProfile SADUserProfile { get; set; }

            private SpotifyPlaylists sadSpotifyPlaylists;
            public SpotifyPlaylists SADSpotifyPlaylists { 
                get
                {
                    if (sadSpotifyPlaylists.Playlists != null) { }
                    return sadSpotifyPlaylists;
                }
            }

            public SpotifyAPIData(ClientAppToken clientAppToken)
            {
                ClientAppToken = clientAppToken;
                SADUserProfile = new UserProfile(clientAppToken);
                sadSpotifyPlaylists = new SpotifyPlaylists(clientAppToken);
            }
        }

        public string CATToken { get; set; }
        public SpotifyToken CATSpotifyToken { get; set; }
        public ApiInfo CATSpotifyAPIInfo { get; set; }
        public SpotifyAPIData CATSpotifyAPIData { get; set; }

        public ClientAppToken()
        {
            CATSpotifyAPIData = new SpotifyAPIData(this);
        }

        public ClientAppToken(string clientAppToken)
        {
            CATToken = clientAppToken;
            CATSpotifyAPIData = new SpotifyAPIData(this);
        }
    }

    public static class Globals
    {
        public static bool IS_DEVELOPMENT_ENVIRONMENT { get; set; } = false;

        public static string MySpotifyClientID = "d0052cf8055246fa8dbd71b5b84284be";
        public static string MySpotifyClientSecret = "a998f5872f93419fb01f3b30c31cb6e3";

        public static List<ClientAppToken> ClientAppTokens = new List<ClientAppToken>();

        public static IPlaylistsApi PlaylistsApi { get; set; }
        public static IUsersProfileApi UsersProfileApi { get; set; }

        public static ClientAppToken GenerateClientAppToken(string clientId, string clientSecret)
        {
            string clientAppToken = "TestAkshaySrinivasan29872397";
            ClientAppToken caToken = new ClientAppToken
            {
                CATToken = clientAppToken,
                CATSpotifyAPIInfo = new ClientAppToken.ApiInfo(clientId, clientSecret)
            };
            ClientAppTokens.Add(caToken);
            return caToken;
        }
    }
}
