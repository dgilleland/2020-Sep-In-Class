/*
Post-Deployment Script Template                            
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.        
 Use SQLCMD syntax to include a file in the post-deployment script.            
 Example:      :r .\myfile.sql                                
 Use SQLCMD syntax to reference a variable in the post-deployment script.        
 Example:      :setvar TableName MyTable                            
               SELECT * FROM [$(TableName)]                    
--------------------------------------------------------------------------------------
*/

INSERT INTO [BuildVersion](Major, Minor, Build, ReleaseDate)
VALUES (0,1,0,GETDATE())
GO

INSERT INTO GovernanceModels(EffectiveDate, TermLength, TermModel, YearStart)
VALUES ('Sep 1, 2016', 15, 2, 9)
GO

INSERT INTO CreditModels(GovernanceId, [Hours], Credits)
VALUES (1, 30, 3.0),
       (1, 45, 3.0),
       (1, 60, 3.0),
       (1, 60, 4.5),
       (1, 75, 4.5),
       (1, 90, 4.5),
       (1, 120, 6.0)
GO

INSERT INTO ProgramsOfStudy(ProgramName, GovernanceModel, Accreditation, CreditsThreshold)
VALUES ('Software Application Developer', 1, 2, 72)
GO

DECLARE @Program int
SET @Program = (SELECT Id FROM ProgramsOfStudy WHERE ProgramName = 'Software Application Developer')
INSERT INTO Courses(Number, [Name], [Hours], [Credits], InitialTerm, ProgramId)
VALUES ('PROG-0101', 'Programming Fundamentals in OOP', 60, 3.0, 1, @Program),
       ('PROG-0105', 'Database Fundamentals', 60, 3.0, 1, @Program),
       ('PROG-0108', 'Introduction to Web Design', 60, 3.0, 1, @Program),
       ('CORE-0113', 'Communications', 60, 3.0, 1, @Program),
       ('PROG-0221', 'Introduction to API Development', 60, 3.0, 2, @Program),
       ('PROG-0307', 'Applied Domain Driven Development', 60, 3.0, 3, @Program)
GO

INSERT INTO Prerequisites(CourseId, PrerequisiteCourseId)
VALUES ((SELECT C.CourseId  FROM Courses AS C WHERE [Number] = 'PROG-0221'),
        (SELECT C.CourseId  FROM Courses AS C WHERE [Number] = 'PROG-0101')),

       ((SELECT C.CourseId  FROM Courses AS C WHERE [Number] = 'PROG-0221'),
        (SELECT C.CourseId  FROM Courses AS C WHERE [Number] = 'PROG-0105')),

       ((SELECT C.CourseId  FROM Courses AS C WHERE [Number] = 'PROG-0221'),
        (SELECT C.CourseId  FROM Courses AS C WHERE [Number] = 'PROG-0108')),

       ((SELECT C.CourseId  FROM Courses AS C WHERE [Number] = 'PROG-0307'),
        (SELECT C.CourseId  FROM Courses AS C WHERE [Number] = 'PROG-0221'))
GO
