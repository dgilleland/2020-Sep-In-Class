CREATE TABLE [dbo].[CourseOfferings]
(
    [CourseOfferingId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CourseId] INT NOT NULL, 
    [AcademicTermId] INT NOT NULL, 
    [SectionName] VARCHAR(5) NULL, 
    CONSTRAINT [FK_CourseOfferings_AcademicTerms] FOREIGN KEY ([AcademicTermId]) REFERENCES [AcademicTerms]([AcademicTermId]), 
    CONSTRAINT [FK_CourseOfferings_Courses] FOREIGN KEY ([CourseId]) REFERENCES [Courses]([CourseId])
)
