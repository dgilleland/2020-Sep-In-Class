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
            using(var context = new ChinookContext())
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
            using(var context = new ChinookContext()) // context is our virtual database
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
    }
}
