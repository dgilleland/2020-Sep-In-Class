<Query Kind="Expression">
  <Connection>
    <ID>3581523b-0d23-4b3f-a580-3704423dac07</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

from line in InvoiceLines
where line.Invoice.CustomerId == 10
//select line
group line by line.Track.Album
.Artist
into albumTracks
select new
{
	albumTracks.Key,
	//Album = albumTracks.Key.Title,
	//Artist = albumTracks.Key.Artist.Name,
	Tracks = from item in albumTracks
			 orderby item.Track.Name
	         select new
			 {
			 	Track = item.Track.Name,
				RunningTime = item.Track.Milliseconds / 1000,
				Album = item.Track.Album.Title
			 }
}
