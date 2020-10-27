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

int customerId = 10; // Make as Parameter
var customer = Customers.Single(c => c.CustomerId == customerId);
var allTracks = from sale in InvoiceLines
                where sale.Invoice.CustomerId == customerId
				select sale;
allTracks.Dump();
var allAlbums = allTracks.Select(t => t.Track.Album.AlbumId).Distinct().Count();
var allArtists = allTracks.Select(t => t.Track.Album.Artist.ArtistId).Distinct().Count();
var result = new // PlaylistCounts
{
	AllTracks = allTracks.Count(),
	Albums = allAlbums,
	Artists = allArtists

};

result.Dump();
