using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookTunes.ViewModels
{
    /// <summary>
    /// Represents simple summary information for a song
    /// </summary>
    public class SongSummary
    {
        /// <summary>Title of the song/track</summary>
        public string Name { get; set; }
        /// <summary>Length of the song/track in seconds</summary>
        public int RunningTime { get; set; }
    }

    /// <summary>
    /// Represents simple summary information for a song with the name of the album
    /// </summary>
    /// <seealso cref="ChinookTunes.ViewModels.SongSummary" />
    public class AlbumSong : SongSummary
    {
        public string Album { get; set; }
    }

    /// <summary>
    /// Represents simple summary information for a song with the name of the album and the artist
    /// </summary>
    /// <seealso cref="ChinookTunes.ViewModels.AlbumSong" />
    public class ArtistAlbumSong : AlbumSong
    {
        public string Artist { get; set; }
    }

    /// <summary>
    /// A playlist of songs on a given album
    /// </summary>
    public class AlbumTracks
    {
        public string Album { get; set; }
        public string Artist { get; set; }
        public IEnumerable<SongSummary> Tracks { get; set; }
    }
    /// <summary>
    /// A playlist of songs by a given artist
    /// </summary>
    public class ArtistTracks
    {
        public string Artist { get; set; }
        public IEnumerable<AlbumSong> Tracks { get; set; }
    }
}
