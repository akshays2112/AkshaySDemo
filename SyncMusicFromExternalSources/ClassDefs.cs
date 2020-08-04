using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncMusicFromExternalSources
{
    public class MyPlaylist
    {
        public class MyPlayListItem
        {
            public string Title { get; set; }
            public string Id { get; set; }

            public MyPlayListItem(string title, string id)
            {
                Title = title;
                Id = id;
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

    class UserPlaylist
    {
        public class UserPlaylistTrack
        {
            public string Name { get; set; }
            public int Index { get; set; }

            public UserPlaylistTrack(string name, int index)
            {
                Name = name;
                Index = index;
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
        public List<CustomArtist> Artists { get; set; }

        public CustomTrack(string name, string id)
        {
            Name = name;
            ID = id;
            Artists = new List<CustomArtist>();
        }
    }
}
