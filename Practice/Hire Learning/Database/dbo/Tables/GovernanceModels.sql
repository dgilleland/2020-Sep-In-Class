CREATE TABLE [dbo].[GovernanceModels]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [EffectiveDate] DATE NOT NULL, 
    [RetiredOn] DATE NULL, 
    [TermModel] TINYINT NOT NULL DEFAULT 2, 
    [YearStart] TINYINT NOT NULL DEFAULT 9, 
    [TermLength] TINYINT NOT NULL DEFAULT 15
)
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'1 = Semester, 2 = Trimester, 3 = Quarter',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'GovernanceModels',
    @level2type = N'COLUMN',
    @level2name = N'TermModel'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'TermLength is expressed in weeks',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'GovernanceModels',
    @level2type = N'COLUMN',
    @level2name = N'TermLength'

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Month Number',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'GovernanceModels',
    @level2type = N'COLUMN',
    @level2name = N'YearStart'