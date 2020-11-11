using SpotifyApi.NetCore;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EpicAkSAuthenticationPages
{
    public class UnityToken
    {
        public class ApiInfo
        {
            public string ClientId { get; set; }
            public string ClientSecret { get; set; }
        }

        public class SpotifyToken
        {
            public string access_token { get; set; }
        }

        public class SpotifyAPIData
        {
            public class UserProfile
            {
                public UnityToken UnityToken { get; set; }

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

                public UserProfile(UnityToken unityToken)
                {
                    UnityToken = unityToken;
                }

                private void Init()
                {
                    string accessToken = UnityToken?.UTSpotifyToken?.access_token;
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
                public UnityToken UnityToken { get; set; }

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

                public SpotifyPlaylists(UnityToken unityToken)
                {
                    UnityToken = unityToken;
                }

                public void Init()
                {
                    string accessToken = UnityToken?.UTSpotifyToken?.access_token;
                    string userID = UnityToken?.UTSpotifyAPIData?.SADUserProfile?.ID;
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

            public UnityToken UnityToken { get; set; }

            public UserProfile SADUserProfile { get; set; }

            private SpotifyPlaylists sadSpotifyPlaylists;
            public SpotifyPlaylists SADSpotifyPlaylists { 
                get
                {
                    if (sadSpotifyPlaylists.Playlists != null) { }
                    return sadSpotifyPlaylists;
                }
            }

            public SpotifyAPIData(UnityToken unityToken)
            {
                UnityToken = unityToken;
                SADUserProfile = new UserProfile(unityToken);
                sadSpotifyPlaylists = new SpotifyPlaylists(unityToken);
            }
        }

        public string UTUnityToken { get; set; }
        public SpotifyToken UTSpotifyToken { get; set; } = new SpotifyToken();
        public ApiInfo UTSpotifyAPIInfo { get; set; } = new ApiInfo {
            ClientId = "d0052cf8055246fa8dbd71b5b84284be",
            ClientSecret = "a998f5872f93419fb01f3b30c31cb6e3"
        };
        public SpotifyAPIData UTSpotifyAPIData { get; set; }

        public UnityToken(string unityToken)
        {
            UTUnityToken = unityToken;
            UTSpotifyAPIData = new SpotifyAPIData(this);
        }
    }

    public static class Globals
    {
        public static bool IS_DEVELOPMENT_ENVIRONMENT { get; set; } = false;

        public static string MySpotifyClientID = "d0052cf8055246fa8dbd71b5b84284be";
        public static string MySpotifyClientSecret = "a998f5872f93419fb01f3b30c31cb6e3";

        public static List<UnityToken> UnityTokens = new List<UnityToken>();

        public static IPlaylistsApi PlaylistsApi { get; set; }
        public static IUsersProfileApi UsersProfileApi { get; set; }
    }
}
