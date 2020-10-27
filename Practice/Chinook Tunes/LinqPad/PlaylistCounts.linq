<Query Kind="Statements">
  <Connection>
    <ID>3581523b-0d23-4b3f-a580-3704423dac07</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

int customerId = 10; // Make as Parameter in my BLL method

// this is the only Linq Query Syntax statement I need
var allTracks = from sale in InvoiceLines // Each InvoiceLine is for a single Track purchase
                where sale.Invoice.CustomerId == customerId
				select sale;
//allTracks.Dump();

// I use Linq Method Syntax to get specific Albumn/Artist info for counting
var allAlbums = allTracks.Select(t => t.Track.Album.AlbumId) // to select only AlbumIds
                .Distinct().Count(); // .Distinct() to get rid of duplicates
var allArtists = allTracks.Select(t => t.Track.Album.Artist.ArtistId) // to select only ArtistIds
                .Distinct().Count(); // .Distinct() to get rid of duplicates
var result = new // PlaylistCounts
{
	AllTracks = allTracks.Count(),
	Albums = allAlbums,
	Artists = allArtists
};

result.Dump();
