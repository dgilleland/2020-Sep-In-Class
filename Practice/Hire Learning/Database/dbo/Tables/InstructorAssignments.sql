CREATE TABLE [dbo].[InstructorAssignments]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [InstructorId] UNIQUEIDENTIFIER NOT NULL, 
    [CourseOfferingId] INT NOT NULL, 
    [Active] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [FK_InstructorAssignments_Instructors] FOREIGN KEY ([InstructorId]) REFERENCES [Instructors]([InstructorId]), 
    CONSTRAINT [FK_InstructorAssignments_CourseOfferings] FOREIGN KEY ([CourseOfferingId]) REFERENCES [CourseOfferings]([CourseOfferingId])
)
