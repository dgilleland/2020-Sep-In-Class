CREATE TABLE [dbo].[AcademicTerms]
(
    [AcademicTermId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Term] VARCHAR(20) NOT NULL, 
    [StartDate] DATE NOT NULL, 
    [EndDate] DATE NOT NULL, 
    [WeeksDuration] TINYINT NOT NULL
)
