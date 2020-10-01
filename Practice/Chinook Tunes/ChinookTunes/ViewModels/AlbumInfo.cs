using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookTunes.ViewModels
{
    /// <summary>
    /// The AlbumInfo class represents music album data used for displaying and modifying information in our database.
    /// </summary>
    public class AlbumInfo
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int ArtistID { get; set; }
        public string ArtistName { get; set; }

        public IEnumerable<string> Songs { get; set; }
        // And any other properties your db has on the Album table...
    }
}
