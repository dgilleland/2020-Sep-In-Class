using ChinookTunes.DAL;
using ChinookTunes.Entities;
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
    public class AlbumManagement
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<AlbumInfo> ListAlbums()
        {
            using (var context = new ChinookContext())
            {
                var result = from record in context.Albums
                             select new AlbumInfo
                             {
                                 ID = record.AlbumId,
                                 Title = record.Title,
                                 ArtistID = record.ArtistId,
                                 ArtistName = record.Artist.Name
                                 //                 \ NAV /
                             };
                return result.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void AddAlbum(AlbumInfo info)
        {
            using (var context = new ChinookContext())
            {
                // Create a new Album from the AlbumInfo view-model class
                var newItem = new Album // Album is the Entity class
                {
                    ArtistId = info.ArtistID,
                    Title = info.Title
                    // Add any other property values for the Album
                };
                //var added = 
                    context.Albums.Add(newItem);
                context.SaveChanges();
            }
            //throw new NotImplementedException("Add functionality not yet implemented");
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public void UpdateAlbum(AlbumInfo info)
        {
            throw new NotImplementedException("Update functionality not yet implemented");
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DeleteAlbum(AlbumInfo info)
        {
            throw new NotImplementedException("Delete functionality not yet implemented");
        }
    }
}
