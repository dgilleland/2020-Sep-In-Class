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
        #region Query Methods
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

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<SelectionItem> ListArtists()
        {
            using (var context = new ChinookContext())
            {
                //  results is IQueryable<T>, which is like a "Collection" of
                var results = from row in context.Artists  // Go through all the Artists
                    // foreach(var row in context.Artists) // Loop through all the Artists
                              select new SelectionItem // Create a SelectionItem object using
                              {
                                  DisplayText = row.Name,  // Name of the Artist
                                  IDValue = row.ArtistId.ToString() // ID of the Artist
                              };
                return results.ToList(); // Convert the IQueryable<T> to a List<T>
            }
        }
        #endregion

        #region Command Methods - Insert/Update/Delete
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
            using (var context = new ChinookContext())
            {
                // 1) Lookup the existing Album data from the database
                var existing = context.Albums.Find(info.ID); // Look it up based on the AlbumInfo.ID
                // 2) Change the property values for the Album
                existing.Title = info.Title;
                existing.ArtistId = info.ArtistID;
                // ... and any other properties
                // 3) Tell the DbContext that I am modifying the album
                context.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                // 4) Issue the update on the database
                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DeleteAlbum(AlbumInfo info)
        {
            using (var context = new ChinookContext())
            {
                var existing = context.Albums.Find(info.ID);
                context.Albums.Remove(existing);
                context.SaveChanges();
            }
        }
        #endregion
    }
}
