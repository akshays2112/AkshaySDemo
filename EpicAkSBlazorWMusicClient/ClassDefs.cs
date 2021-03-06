﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EpicAkSBlazorWMusicClient
{
    public partial class Globals
    {
        public static SpotifyAccessToken SpotifyAccessToken;
        public static GoogleApisYoutubeAccessToken GoogleApisYoutubeAccessToken;
        public static int DivIndex = 0;
        //rapidapi.com - Only has Spotify metadata per artist. Tried to put in a request to RapidAPI for metadata per Track/Song.
        //public static string RapidApi_ApiKey = "b4b7265557msh952992d0f3d7bfap1cc933jsn0e90675d30c8";
        public static string RedirectUri;

        public static List<UserPlaylist> SpotifyUserPlayLists = new List<UserPlaylist>();
    }

    public class SpotifyAccessToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }
        public long expires_in { get; set; }
        public string refresh_token { get; set; }
    }

    public class GoogleApisYoutubeAccessToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public long expires_in { get; set; }
    }

    public class MyPlaylist
    {
        public class MyPlayListItem
        {
            public string Title { get; set; }
            public string Id { get; set; }
            public string CleanedUpTitle { get; set; }

            public MyPlayListItem(string title, string id)
            {
                Title = title;
                Id = id;
                if (!string.IsNullOrWhiteSpace(title))
                {
                    string tmpTitle = title;
                    Regex regex = new Regex("[^a-zA-Z0-9 ]{1,}");
                    tmpTitle = regex.Replace(tmpTitle, "");
                    regex = new Regex("[ ]{2,}");
                    CleanedUpTitle = regex.Replace(tmpTitle, " ").Replace("Official Music Video", "").Replace("Official Video", "");
                }
            }
        }

        public string Title { get; set; }
        public string ID { get; set; }
        public long Count { get; set; }
        public List<MyPlayListItem> MyPlayListItems { get; set; }

        public MyPlaylist(string title, string id, long? count)
        {
            Title = title;
            ID = id;
            Count = count ?? 0;
            MyPlayListItems = new List<MyPlayListItem>();
        }
    }

    public class UserPlaylist
    {
        public class UserPlaylistTrack
        {
            public string Name { get; set; }
            public string Id { get; set; }

            public UserPlaylistTrack(string name, string id)
            {
                Name = name;
                Id = id;
            }
        }

        public string Name { get; set; }
        public int Total { get; set; }
        public string Id { get; set; }
        public List<UserPlaylistTrack> UserPlaylistTracks { get; set; }

        public UserPlaylist(string name, int total, string id)
        {
            Name = name;
            Total = total;
            Id = id;
            UserPlaylistTracks = new List<UserPlaylistTrack>();
        }
    }

    public class CustomTrack
    {
        public class CustomArtist
        {
            public string Name { get; set; }
            public string ID { get; set; }

            public CustomArtist(string name, string id)
            {
                Name = name;
                ID = id;
            }
        }

        public string Name { get; set; }
        public string ID { get; set; }
        public int Popularity { get; set; }
        public List<CustomArtist> Artists { get; set; }

        public CustomTrack(string name, string id)
        {
            Name = name;
            ID = id;
            Artists = new List<CustomArtist>();
        }

        public CustomTrack(string name, string id, int popularity)
        {
            Name = name;
            ID = id;
            Popularity = popularity;
            Artists = new List<CustomArtist>();
        }
    }

    public class MyYoutubePlaylist
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("etag")]
        public string Etag { get; set; }

        [JsonProperty("nextPageToken")]
        public string NextPageToken { get; set; }

        [JsonProperty("prevPageToken")]
        public string PrevPageToken { get; set; }

        [JsonProperty("pageInfo")]
        public PageInfo PageInfo { get; set; }

        [JsonProperty("items")]
        public Item[] Items { get; set; }
    }

    public class Item
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("snippet")]
        public Snippet Snippet { get; set; }

        [JsonProperty("contentDetails")]
        public ContentDetails ContentDetails { get; set; }
    }

    public class ContentDetails
    {
        [JsonProperty("itemCount")]
        public long ItemCount { get; set; }
    }

    public class Snippet
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class PageInfo
    {
        [JsonProperty("totalResults")]
        public long TotalResults { get; set; }

        [JsonProperty("resultsPerPage")]
        public long ResultsPerPage { get; set; }
    }
}
