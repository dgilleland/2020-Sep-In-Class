using ChinookTunes.DAL;
using ChinookTunes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookTunes.BLL
{
    public class CustomerController
    {
        public List<SelectionItem> ListAllCustomers()
        {
            using (var context = new ChinookContext())
            {
                var result = from person in context.Customers
                                 // ![](../../SongListings.png;;0,0,370,34)
                             select new SelectionItem // my ViewModel class for drop-downs
                             {
                                 IDValue = person.CustomerId.ToString(),
                                 DisplayText = person.FirstName + " " + person.LastName
                             };
                return result.ToList();
            }
        }

        public PlaylistCounts GetPlaylistCounts(int customerId)
        {
            using (var context = new ChinookContext()) // context is our virtual database
            {
                // this is the only Linq Query Syntax statement I need
                var allTracks = from sale in context.InvoiceLines // Each InvoiceLine is for a single Track purchase
                                where sale.Invoice.CustomerId == customerId
                                select sale;

                // I use Linq Method Syntax to get specific Albumn/Artist info for counting
                var allAlbums = allTracks.Select(t => t.Track.Album.AlbumId) // to select only AlbumIds
                                .Distinct().Count(); // .Distinct() to get rid of duplicates
                var allArtists = allTracks.Select(t => t.Track.Album.Artist.ArtistId) // to select only ArtistIds
                                .Distinct().Count(); // .Distinct() to get rid of duplicates
                var result = new PlaylistCounts
                {
                    AllTracks = allTracks.Count(),
                    Albums = allAlbums,
                    Artists = allArtists
                };
                return result;
            }
        }

        public List<ArtistAlbumSong> AllTracksByCustomer(int customerId)
        {
            using (var context = new ChinookContext())
            {
                var result = from line in context.InvoiceLines
                             where line.Invoice.CustomerId == customerId
                             orderby line.Track.Name
                             select new ArtistAlbumSong
                             {
                                 Name = line.Track.Name,
                                 RunningTime = line.Track.Milliseconds / 1000,
                                 Artist = line.Track.Album.Artist.Name,
                                 Album = line.Track.Album.Title
                             };
                return result.ToList();
            }
        }

        public List<AlbumTracks> AllTracksByAlbum(int customerId)
        {
            // ![](../../SongListings.png;;0,177,370,271;,0.02949)
            using (var context = new ChinookContext())
            {
                var result = from line in context.InvoiceLines // 1) Apply my database to the query:  context.InvoiceLines
                             where line.Invoice.CustomerId == customerId
                             orderby line.Track.Album.Title
                             group line by line.Track.Album into albumTracks
                             select new AlbumTracks // 3) Make a view model for the Album w. tracks
                             {
                                 Album = albumTracks.Key.Title,
                                 Artist = albumTracks.Key.Artist.Name, // optional, possibly useful
                                 Tracks = from item in albumTracks
                                          orderby item.Track.Name
                                          select new SongSummary // 2) Make a view model for the song's name/time
                                          {
                                              Name = item.Track.Name,
                                              RunningTime = item.Track.Milliseconds / 1000
                                          }
                             };
                return result.ToList();
            }
        }

        public List<ArtistTracks> AllTracksByArtist(int customerId)
        {
            using (var context = new ChinookContext())
            {
                var result = from line in context.InvoiceLines
                             where line.Invoice.CustomerId == customerId
                             orderby line.Track.Album.Artist.Name
                             group line by line.Track.Album.Artist.Name into albumTracks
                             select new ArtistTracks
                             {
                                 Artist = albumTracks.Key,
                                 Tracks = from item in albumTracks
                                          orderby item.Track.Name
                                          select new AlbumSong
                                          {
                                              Name = item.Track.Name,
                                              RunningTime = item.Track.Milliseconds / 1000,
                                              Album = item.Track.Album.Title
                                          }
                             };
                return result.ToList();
            }
        }
    }
}
