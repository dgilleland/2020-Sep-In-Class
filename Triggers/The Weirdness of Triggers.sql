-- The Weirdness of Triggers

IF NOT EXISTS (SELECT [name]
               FROM [master].[sys].databases
               WHERE [name] = N'MyFirstDatabase')
BEGIN
    CREATE DATABASE [MyFirstDatabase]
END
GO

USE [MyFirstDatabase] -- Switch to a specific database for the remainder of this script file
GO

CREATE TABLE SomeTable
(
    SomeTableId     int     IDENTITY PRIMARY KEY NOT NULL,
    SomeText        varchar(30) NOT NULL
)
GO

CREATE OR ALTER TRIGGER SomeTable_Update
ON SomeTable
FOR UPDATE
AS
    IF @@Rowcount > 0
    BEGIN
        IF 1 = 1
        BEGIN
            ROLLBACK TRANSACTION
            RAISERROR('Stopping the update', 16, 1)
        END

        IF @@ERROR <> 0
        BEGIN
            RAISERROR('I just saw my error', 16, 1)
        END

        DELETE SomeTable
        WHERE  SomeTableId IN (1)
    END
RETURN
GO

INSERT INTO SomeTable(SomeText) VALUES ('My Text')
INSERT INTO SomeTable(SomeText) VALUES ('Your Text')

SELECT * FROM SomeTable

UPDATE SomeTable
SET    SomeText = 'Other Text'
WHERE  SomeTableId IN (1, 3, 5, 7)

SELECT * FROM SomeTable
