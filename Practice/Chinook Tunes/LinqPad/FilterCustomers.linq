<Query Kind="Program">
  <Connection>
    <ID>3581523b-0d23-4b3f-a580-3704423dac07</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

void Main()
{
	var result =
		from row in Customers
		where row.Email.EndsWith(".com")
		select new Person
		     //  new // Anonymous data type
		// The initializer list determines what properties will exist in the anonymous type
		{
            // By default, the property name is "inferred" from the property of the entity
			//row.FirstName, // .FirstName
			FirstName = row.FirstName, // Explicitly use the property name as assignment
			Surname = row.LastName // .Surname
		};
	result.Dump("Filtered List of Customers");
}

// Define other methods, classes and namespaces here
public class Person
{
	public string FirstName {get;set;}
	public string Surname{get;set;}
}