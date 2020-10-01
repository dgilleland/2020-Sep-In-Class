using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookTunes.ViewModels
{
    /// <summary>
    /// View-Model Representation of the <see cref="ChinookTunes.Entities.Track"/> data.
    /// </summary>

    public class Songs
    {
        public int TrackID { get; set; }
        public string Title { get; set; }
        public string Composer { get; set; }
        public int Milliseconds { get; set; }
        public int Bytes { get; set; }
        public decimal Price { get; set; }

    }
}
