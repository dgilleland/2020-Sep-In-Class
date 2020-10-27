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

from person in Customers
select new // SelectionItem // my ViewModel class for drop-downs
{
	IDValue = person.CustomerId.ToString(),
	DisplayText = person.FirstName +  " " + person.LastName
}