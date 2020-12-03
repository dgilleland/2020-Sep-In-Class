CREATE TABLE [dbo].[Students] (
    [IdNumber]          UNIQUEIDENTIFIER NOT NULL,
    [GivenNames]        NVARCHAR (50) NULL,
    [Surname]           NVARCHAR (50) NULL,
    [Email]             NVARCHAR (200) NULL,
    [SchoolIdentifier]  NVARCHAR (36) NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED ([IdNumber] ASC)
);


