CREATE TABLE [dbo].[EvaluationComponents]
(
    [EvaluationComponentId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(30) NOT NULL, 
    [Weight] TINYINT NOT NULL, 
    [RequiredPass] BIT NOT NULL DEFAULT 0, 
    [EvaluationGroupId] INT NULL, 
    [CourseId] INT NOT NULL,
    CONSTRAINT [FK_EvaluationComponents_EvaluationGroups] FOREIGN KEY ([EvaluationGroupId]) REFERENCES [EvaluationGroups]([EvaluationGroupId]),
    CONSTRAINT [FK_EvaluationComponents_Courses] FOREIGN KEY ([CourseId]) REFERENCES [Courses]([CourseId])
)
