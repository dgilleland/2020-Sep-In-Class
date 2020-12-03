CREATE TABLE [dbo].[ProgramsOfStudy]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(100, 1), 
    [ProgramName] NVARCHAR(120) NOT NULL, 
    [Accreditation] TINYINT NULL, 
    [Active] BIT NOT NULL DEFAULT 1, 
    [GovernanceModel] INT NULL, 
    [CreditsThreshold] TINYINT NULL, 
    CONSTRAINT [FK_ProgramsOfStudy_GovernanceModels] FOREIGN KEY ([GovernanceModel]) REFERENCES [GovernanceModels]([Id])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'0 = None, 1 = Certificate, 2 = Diploma, 3 = Applied Degree, 4 = Degree',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'ProgramsOfStudy',
    @level2type = N'COLUMN',
    @level2name = N'Accreditation'