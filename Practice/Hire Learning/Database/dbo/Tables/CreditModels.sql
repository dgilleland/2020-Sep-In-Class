CREATE TABLE [dbo].[CreditModels]
(
    [GovernanceId] INT NOT NULL , 
    [Hours] TINYINT NOT NULL, 
    [Credits] DECIMAL(3, 1) NOT NULL, 
    PRIMARY KEY ([GovernanceId], [Hours], [Credits]), 
    CONSTRAINT [FK_CreditModels_Governance] FOREIGN KEY ([GovernanceId]) REFERENCES [GovernanceModels]([Id])
)
