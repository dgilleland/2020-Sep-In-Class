CREATE TABLE [dbo].[EvaluationGroups]
(
    [EvaluationGroupId] INT NOT NULL PRIMARY KEY IDENTITY,
    [Name] NVARCHAR(30) NOT NULL, 
    [Weight] TINYINT NOT NULL, 
    [RequiredPass] BIT NOT NULL DEFAULT 0
)
