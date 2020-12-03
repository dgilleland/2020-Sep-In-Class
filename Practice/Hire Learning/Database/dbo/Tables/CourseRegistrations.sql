CREATE TABLE [dbo].[CourseRegistrations]
(
    [CourseRegistrationId] INT NOT NULL PRIMARY KEY IDENTITY,
    [CourseOfferingId]  INT            NOT NULL, 
    [StudentId] UNIQUEIDENTIFIER NOT NULL,
)
