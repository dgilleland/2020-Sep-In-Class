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
//orderby line.Track.Name
//select new
//{
//	Name = line.Track.Name,
//	Time = line.Track.Milliseconds / 1000,
//	Artist = line.Track.Album.Artist.Name,
//	Album = line.Track.Album.Title
//}
group line by line.Track.Album
//.Artist
into albumTracks
select new
{
	//albumTracks.Key,
	//Artists = albumTracks.Key.Name,
	Album = albumTracks.Key.Title + "(by " + albumTracks.Key.Artist.Name + ")",
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
