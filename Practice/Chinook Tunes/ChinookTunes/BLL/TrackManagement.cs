using ChinookTunes.DAL;
using ChinookTunes.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookTunes.BLL
{
    [DataObject]
    public class TrackManagement
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Song> ListAllTracks()
        {
            using(var context = new ChinookContext())
            {
                var result = from music in context.Tracks
                             select new Song
                             {
                                 // Set the properties of the Song object to the data from the Track entity
                             };
                return result.ToList();
            }
        }
    }
}
