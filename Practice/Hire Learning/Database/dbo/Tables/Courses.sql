CREATE TABLE [dbo].[Courses]
(
    [CourseId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Number] CHAR(9) NOT NULL, 
    [Name] VARCHAR(70) NOT NULL, 
    [Hours] TINYINT NOT NULL, 
    [Credits] DECIMAL(3, 1) NOT NULL, 
    [InitialTerm] TINYINT NOT NULL,
    [IsElective] BIT NOT NULL DEFAULT 0, 
    [ProgramId] INT NULL, 
    CONSTRAINT [FK_Courses_ProgramOfStudy] FOREIGN KEY ([ProgramId]) REFERENCES [ProgramsOfStudy]([Id]),
    CONSTRAINT [UX_Courses_Number] UNIQUE ([Number]),
    CONSTRAINT [CK_Courses_Number] CHECK ([Number] LIKE '[a-Z][a-Z][a-Z][a-Z]-[0-9][0-9][0-9][0-9]')
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Course Number is a four-letter/four-digit identifier.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Courses',
    @level2type = N'COLUMN',
    @level2name = N'Number'