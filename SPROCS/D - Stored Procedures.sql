--Stored Procedures (Sprocs)
-- Global Variables - @@IDENTITY, @@ROWCOUNT, @@ERROR
-- Other global variables can be found here:
--  https://code.msdn.microsoft.com/Global-Variables-in-SQL-749688ef
USE [A01-School]
GO

/*

|       | @@IDENTITY    | @@ROWCOUNT    | @@ERROR
| SELECT|   no          |   yes         |   no
| INSERT|   maybe       |   yes         |   yes
| UPDATE|   no          |   yes         |   yes
| DELETE|   no          |   yes         |   yes

*/


/*
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = N'PROCEDURE' AND ROUTINE_NAME = 'SprocName')
    DROP PROCEDURE SprocName
GO
CREATE PROCEDURE SprocName
    -- Parameters here
AS
    -- Body of procedure here
RETURN
GO
*/

-- 1. Create a stored procedure called AddPosition that will accept a Position Description (varchar 50). Return the primary key value that was database-generated as a result of your Insert statement. Also, ensure that the supplied description is not NULL and that it is at least 5 characters long. Make sure that you do not allow a duplicate position name.
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = N'PROCEDURE' AND ROUTINE_NAME = 'AddPosition')
    DROP PROCEDURE AddPosition
GO
CREATE PROCEDURE AddPosition
    -- Parameters here
    @Description    varchar(50) -- Max of 50 characters
AS
    -- Body of procedure here
    IF @Description IS NULL
    BEGIN -- {
        RAISERROR('Description is required', 16, 1) -- Throw an exception
    END   -- }
    ELSE
    BEGIN -- {
        IF LEN(@Description) < 5 -- I didn't need to identify the max
        BEGIN -- {
            RAISERROR('Description must be between 5 and 50 characters', 16, 1)
        END   -- }
        ELSE
        BEGIN -- {
            IF EXISTS(SELECT * FROM Position WHERE PositionDescription = @Description)
            BEGIN -- {
                RAISERROR('Duplicate positions are not allowed', 16, 1)
            END   -- }
            ELSE
            BEGIN -- { -- This BEGIN/END is needed, because of two SQL statements
                INSERT INTO Position(PositionDescription)
                VALUES (@Description)
                -- Send back the database-generated primary key
                SELECT @@IDENTITY AS 'NewPositionID' -- This is a global variable
            END   -- }
        END   -- }
    END   -- }
RETURN
GO

-- Let's test our AddPosition stored procedure
SELECT * FROM [Position]
INSERT INTO Position(PositionDescription) VALUES ('Substitute')
SELECT @@IDENTITY AS 'PositionCode'
SELECT * FROM [Position]

EXEC AddPosition 'The Boss'
EXEC AddPosition NULL -- This should result in an error being raised
EXEC AddPosition 'Me' -- This should result in an error being raised
EXEC AddPosition 'The Boss' -- This should result in an error as well (a duplicate)
-- This long string gets truncated at the parameter, because the parameter size is 50
EXEC AddPosition 'The Boss of everything and everyone, everywhere and all the time, both past present and future, without any possible exception. Unless, of course, I''m not...'
SELECT * FROM Position
INSERT INTO Position(PositionDescription) VALUES (NULL)
-- Did that failed insert affect the global variable?
SELECT @@IDENTITY

EXEC AddPosition 'The Janitor'
SELECT * FROM Position
-- WTH??? - Why that happened?
-- Any attempt to insert a row into a table with an Identity column
-- will force the IDENTITY value for that table to increment
-- whether or not the insert succeeds or fails. Why? It's because
-- the IDENTITY constraint on the column is a guarantee of a unique
-- value. While the IDENTITY value is automatically incremented on
-- each INSERT attempt, the @@IDENTITY will only be changed if the
-- INSERT is successful.
INSERT INTO Position(PositionDescription) VALUES ('HR')
SELECT @@IDENTITY
SELECT * FROM Position

-- Let's switch to trying to insert into the PaymentType table
INSERT INTO PaymentType(PaymentTypeDescription) VALUES ('Gift Cards')
SELECT @@IDENTITY
SELECT * FROM PaymentType

INSERT INTO Position(PositionDescription) VALUES ('This is a really long bit of text because I am doing all of this on the fly without any notes because I have taught a long time and thisis really too early to trythiis')
SELECT @@IDENTITY


GO

ALTER PROCEDURE AddPosition
    -- Parameters here
    @Description    varchar(500) -- Just to "allow" a larger value, but check the length later
AS
    -- Body of procedure here
    IF @Description IS NULL
    BEGIN -- {
        RAISERROR('Description is required', 16, 1) -- Throw an exception
    END   -- }
    ELSE
    BEGIN -- {
        IF LEN(@Description) < 5 -- OR Len(@Description) > 50
        BEGIN -- {
            RAISERROR('Description must be between 5 and 50 characters', 16, 1)
        END   -- }
        ELSE
        BEGIN -- {
            IF EXISTS(SELECT * FROM Position WHERE PositionDescription = @Description)
            BEGIN -- {
                RAISERROR('Duplicate positions are not allowed', 16, 1)
            END   -- }
            ELSE
            BEGIN -- { -- This BEGIN/END is needed, because of two SQL statements
                INSERT INTO Position(PositionDescription)
                VALUES (@Description)
                -- Send back the database-generated primary key
                SELECT @@IDENTITY -- This is a global variable
            END   -- }
        END   -- }
    END   -- }
RETURN
GO

EXEC AddPosition 'Still the Boss of everything and everyone, everywhere and all the time, both past present and future, without any possible exception. Unless, of course, I''m not...'
SELECT * FROM Position
-- DELETE FROM Position WHERE PositionID = 12

-- 2) Create a stored procedure called LookupClubMembers that takes a club ID and returns the full names of all members in the club.
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = N'PROCEDURE' AND ROUTINE_NAME = 'LookupClubMembers')
    DROP PROCEDURE LookupClubMembers
GO
CREATE PROCEDURE LookupClubMembers
    -- Parameters here
    @ClubId     varchar(10)
AS
    -- Body of procedure here
    IF @ClubId IS NULL OR NOT EXISTS(SELECT * FROM Club WHERE ClubId = @ClubId)
    BEGIN
        RAISERROR('ClubID is invalid/does not exist', 16, 1)
    END
    ELSE
    BEGIN
        SELECT  FirstName + ' ' + LastName AS 'MemberName'
        FROM    Student S
            INNER JOIN Activity A ON A.StudentID = S.StudentID
        WHERE   A.ClubId = @ClubId
        SELECT @@ROWCOUNT
    END
RETURN
GO

-- Test the above sproc
EXEC LookupClubMembers 'CHESS'
SELECT @@ROWCOUNT
EXEC LookupClubMembers 'CSS'
EXEC LookupClubMembers 'Drop Out'
EXEC LookupClubMembers 'NASA1'
EXEC LookupClubMembers NULL

-- 3) Create a stored procedure called RemoveClubMembership that takes a club ID and deletes all the members of that club. Be sure that the club exists. Also, raise an error if there were no members deleted from the club.
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = N'PROCEDURE' AND ROUTINE_NAME = 'RemoveClubMembership')
    DROP PROCEDURE RemoveClubMembership
GO
CREATE PROCEDURE RemoveClubMembership
    -- Parameters here
    @ClubId     varchar(10)
AS
    -- Body of procedure here
    IF @ClubId IS NULL OR NOT EXISTS(SELECT * FROM Club WHERE ClubId = @ClubId)
    BEGIN
        RAISERROR('ClubID is invalid/does not exist', 16, 1)
    END
    ELSE
    BEGIN
        DELETE FROM Activity
        WHERE       ClubId = @ClubId
        -- Any Insert/Update/Delete will affect the global @@ROWCOUNT value
        IF @@ROWCOUNT = 0
        BEGIN
            RAISERROR('No members were deleted', 16, 1)
        END
    END
RETURN
GO
-- Test the above sproc...
EXEC RemoveClubMembership NULL
EXEC RemoveClubMembership 'Drop Out'
EXEC RemoveClubMembership 'NASA1'
EXEC RemoveClubMembership 'CSS'
EXEC RemoveClubMembership 'CSS' -- The second time this is run, there will be no members to remove


-- 4) Create a stored procedure called OverActiveMembers that takes a single number: ClubCount. This procedure should return the names of all members that are active in as many or more clubs than the supplied club count.
--    (p.s. - You might want to make sure to add more members to more clubs, seeing as tests for the last question might remove a lot of club members....)
-- TODO: Student Answer Here
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = N'PROCEDURE' AND ROUTINE_NAME = 'OverActiveMembers')
    DROP PROCEDURE OverActiveMembers
GO
CREATE PROCEDURE OverActiveMembers
    @ClubCount  int
AS
    IF @ClubCount IS NULL OR @ClubCount <= 0
        RAISERROR('ClubCount is required and must be positive', 16, 1)
    ELSE
        SELECT  FirstName, LastName
        FROM    Student
        WHERE   StudentID IN
                (SELECT StudentId FROM Activity
                 GROUP BY StudentId
                 HAVING COUNT(StudentID) >= @ClubCount)
RETURN
GO
-- Testing
SELECT * FROM Activity ORDER BY StudentID
SELECT StudentID, COUNT(ClubID) FROM Activity GROUP BY StudentID
EXEC OverActiveMembers 2
EXEC OverActiveMembers 3
EXEC OverActiveMembers 1
EXEC OverActiveMembers 0
EXEC OverActiveMembers -4
EXEC OverActiveMembers NULL


-- 5) Create a stored procedure called ListStudentsWithoutClubs that lists the full names of all students who are not active in a club.
-- TODO: Student Answer Here
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = N'PROCEDURE' AND ROUTINE_NAME = 'ListStudentsWithoutClubs')
    DROP PROCEDURE ListStudentsWithoutClubs
GO
CREATE PROCEDURE ListStudentsWithoutClubs
AS
    SELECT  FirstName + ' ' + LastName AS 'FullName'
    FROM    Student
    WHERE   StudentID NOT IN (SELECT DISTINCT StudentID FROM Activity)
RETURN
GO
EXEC ListStudentsWithoutClubs

SELECT  FirstName + ' ' + LastName AS 'FullName', A.StudentID, ClubId
FROM    Student AS S 
    LEFT OUTER JOIN Activity AS A ON S.StudentID = A.StudentID
WHERE   A.StudentID IS NULL


-- 6) Create a stored procedure called LookupStudent that accepts a partial student last name and returns a list of all students whose last name includes the partial last name. Return the student first and last name as well as their ID.
-- TODO: Student Answer Here
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = N'PROCEDURE' AND ROUTINE_NAME = 'LookupStudent')
    DROP PROCEDURE LookupStudent
GO
CREATE PROCEDURE LookupStudent
    @PartialLastName    varchar(35) -- The column size for LastName
AS
    IF @PartialLastName IS NULL OR LEN(@PartialLastName) = 0
        RAISERROR('Partial last name is required an must be at least a single character', 16, 1)
    ELSE
        SELECT  FirstName, LastName, StudentID
        FROM    Student
        WHERE   LastName LIKE '%' + @PartialLastName + '%'
RETURN
GO
EXEC LookupStudent 'oo'
EXEC LookupStudent ''
EXEC LookupStudent NULL


-- ??) Here's a sample problem that uses @@ERROR. Create a stored procedure called RemoveJobPosition that thakes in the name of the position and deletes it from the Position table. Ensure the supplied name is valid and that it exists. Generate your own error message if the attempted delete fails.
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = N'PROCEDURE' AND ROUTINE_NAME = 'RemoveJobPosition')
    DROP PROCEDURE RemoveJobPosition
GO
CREATE PROCEDURE RemoveJobPosition
    -- Parameters here
    @Description    varchar(50)
AS
    IF @Description IS NULL
        RAISERROR('Job description is required', 16, 1)
    ELSE IF NOT EXISTS (SELECT * FROM Position WHERE PositionDescription = @Description)
        RAISERROR('The supplied job position does not exist', 16, 1)
    ELSE
    BEGIN
        DELETE FROM [Position]
        WHERE PositionDescription = @Description
        -- The above could fail due to the FK constraints
        IF @@ERROR <> 0 -- I have a non-zero error number
        BEGIN
            DECLARE @Msg varchar(80)
            SET @Msg = 'Cannot delete the position "' + @Description + '"'
            RAISERROR(@Msg, 16, 1)
        END
    END
RETURN
GO
SELECT * FROM [Position]
EXEC RemoveJobPosition 'Dean'

-- The following practice problems use global variables

-- 7) Create a stored procedure called AddPaymentType that takes in a description/name for the payment type and adds it to the PaymentType table. Be sure to prevent any duplicate payment types and also make sure the name of the pament type is at least 4 characters long. Return the PaymentTypeID that was generated for the inserted row.

-- 8) Create a stored procedure called RemovePaymentType that takes in the name of the payment type and deletes it from the PaymentType table. Ensure the supplied name is valid and that it exists. Generate your own error message if the attempted delete fails.
