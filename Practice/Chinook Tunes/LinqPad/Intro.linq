<Query Kind="Statements" />

var instructors = new string[]{"Dan", "Don", "Sam", "Bill", "Steve", "Alice" };
instructors.Dump("All Instructors");
List<string> People = new List<string>();
/* Imperative approach to searching */
//     \ variable /  \collection/
foreach (var person in instructors)
//       \string/     \string[]/
{
	if(person.Length > 3) // Filtering
		People.Add(person); // Adding to the results
}
People.Dump("People");

/* LINQ gives us a Declarative approach to searching */
//                     \variable/ \collection/
var result = from string person in instructors
//                \opt/
             where person.Length > 3 // Filtering
			 select person; // Select (add to the result)
result.Dump();
