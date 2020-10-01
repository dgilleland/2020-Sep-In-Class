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
    public class TrackManagment
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Songs> ListAllTracks()
        {
            using (var context = new ChinookContext())
            {
                var result = from music in context.Tracks
                             select new Songs
                             {

                             };
                return result.ToList();
            }
        }   
    }
}
