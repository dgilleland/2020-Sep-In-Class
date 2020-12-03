<Query Kind="Expression">
  <Connection>
    <ID>a9163a36-c1b1-4d5a-976d-774314a6d45d</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>LearnDb</Database>
  </Connection>
</Query>

from item in Courses
where item.ProgramId == 100 // programId
select new // SchoolCourse
{
	CourseId = item.CourseId,
	Number = item.Number,
	Name = item.Name,
	Hours = item.Hours,
	Credits = item.Credits,
	Term = item.InitialTerm,
	IsElective = item.IsElective,
	Prerequisites = from required in item.Prerequisites // rename to CourseBase
	                select new // CourseReference
					{
						CourseId = required.PrerequisiteCourseId, // the course that is required
						Number = required.Prerequisite.Number
					}
}