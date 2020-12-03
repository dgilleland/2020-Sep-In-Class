CREATE TABLE [dbo].[CourseLocations]
(
    [CourseLocationID] INT NOT NULL PRIMARY KEY, 
    [CourseOfferingID] INT NOT NULL, 
    [RoomNumber] VARCHAR(10) NOT NULL, 
    [StartTime] DATETIME NOT NULL, 
    [EndTime] DATETIME NOT NULL, 
    CONSTRAINT [FK_CourseLocations_CourseOfferings] FOREIGN KEY ([CourseOfferingID]) REFERENCES [CourseOfferings]([CourseOfferingID])
)
