using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookTunes.ViewModels
{
    /// <summary>
    /// View-Model representation of the <see cref="ChinookTunes.Entities.Track"/> data.
    /// </summary>
    public class Song
    {
        public int TrackId { get; set; }
        public string Title { get; set; } // Name of the track
        public string Composer { get; set; }
        public int Milliseconds { get; set; }
        public int Bytes { get; set; }
        public decimal Price { get; set; }
        // TODO: Add other properties as needed....
    }
}
