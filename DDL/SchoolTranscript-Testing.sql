/* Testing my SchoolTranscript.sql script */
-- Remember to always start your scripts with a USE statement to be in the right database
USE SchoolTranscript
GO


-- Inserting some Good data
INSERT INTO Students(GivenName, Surname, DateOfBirth, Enrolled)
VALUES ('Stewart', 'Dent', 'JAN 15, 2000', 1)

INSERT INTO Students(GivenName, Surname, DateOfBirth)
VALUES ('Crystal', 'Clear', 'SEP 21, 1999'),
       ('Len', 'Der', 'MAR 8, 1998'),
       ('Ken Tuck', 'Ederby', 'OCT 3, 2001')

INSERT INTO Courses([Number], [Name], Credits, [Hours], Active, Cost)
VALUES ('DMIT-1508', 'Database Fundamentals', 4.5, 90, 1, 800.00)

INSERT INTO StudentCourses(StudentID, CourseNumber, [Year], Term, [Status])
VALUES (2000, 'DMIT-1508', 2020, 'SEP', 'E')


-- Query my table to see the data
SELECT  StudentID, GivenName, Surname, DateOfBirth, Enrolled
FROM    Students

SELECT [Number], [Name], Credits, [Hours], Active, Cost
FROM   Courses

SELECT StudentID, CourseNumber, [Year], Term, [Status]
FROM   StudentCourses
