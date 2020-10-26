/* **********************************************
 * Simple Table Creation - Columns and Primary Keys
 *
 * School Transcript
 *  Version 1.0.0
 *
 * Author: Dan Gilleland
 ********************************************** */
-- Create the database
IF NOT EXISTS (SELECT name FROM master.sys.databases WHERE name = N'SchoolTranscript')
BEGIN
    CREATE DATABASE [SchoolTranscript] -- Will create a new database with some default tables (INFORMATION_SCHEMA.TABLES) inside
END
GO

-- Switch execution context to the database
USE [SchoolTranscript] -- remaining SQL statements will run against the SchoolTranscript database
GO

-- Drop Tables
--  Drop them in the REVERSE ORDER I created them
IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'StudentCourses')
    DROP TABLE StudentCourses
IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Courses')
    DROP TABLE Courses
IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Students')
    DROP TABLE Students

-- SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES

-- Create Tables...
--    Build them in an order that supports the FK constraints
-- (Dan Gilleland:) My personal coding convention is to keep all SQL keywords in UPPER CASE except for data types (lower case)
-- and all object names (Tables, ColumnNames, etc.) as TitleCase.

-- A Table Definition consists of a comma-separated list
-- of Column Definitions and Table Constraints.
-- Column Definitions can include adding one or more constraints
-- to the column. It is a good idea to give a name to your
-- constraints. Use the following prefixes:
-- - PK - Primary Key
-- - FK - Foreign Key
-- - DF - Default
-- - CK - Check
-- Additional prefixes on named items in our table definitions
-- - IX - Indexes
-- - UX - Unique constraints
CREATE TABLE Students -- The default "schema" name is [dbo] - a "schema" is a subset of a database
(
    -- Our column definitions will describe the
    -- name of the column and its data type as well as
    -- any "constraints" or "restrictions" around the data
    -- that can be stored in that column
    StudentID       int
        CONSTRAINT PK_Students_StudentID PRIMARY KEY
        -- A PRIMARY KEY constraint prevents duplicate data
        IDENTITY (2000, 5)
        -- An IDENTITY constraint means that the database server
        -- will take responsibility to put a value in this column
        -- every time a new row is added to the table.
        -- IDENTITY constraints can only be applied to PRIMARY KEY
        -- columns that are of a whole-number numeric type.
        -- The IDENTITY constraint takes two values
        --  - The "seed" or starting value for the first row inserted
        --  - The "increment" or amount by which the values increase
                                NOT NULL,
    GivenName       varchar(50)
        CONSTRAINT CK_Students_GivenName
            CHECK (GivenName LIKE '[A-Z][A-Z]%')
            -- Matches 'Dan' or 'Danny' or 'Jo'
                                NOT NULL,
    Surname         varchar(50)
        CONSTRAINT CK_Students_Surname
            CHECK (Surname LIKE '__%') -- Not as good as [A-Z][A-Z]%
                                       -- Silly matches: 42
                                NOT NULL,
    DateOfBirth     datetime    NOT NULL,
    Enrolled        bit -- Holds values of either 1 or 0
        CONSTRAINT DF_Students_Enrolled
            DEFAULT (1)
        -- A DEFAULT constraint means that if no data is supplied
        -- for this column, it will automatically use the default.
                                NOT NULL
)

CREATE TABLE Courses
(
    [Number]        varchar(10)
        CONSTRAINT PK_Courses_Number PRIMARY KEY
        CONSTRAINT CK_Courses_Number
            CHECK ([Number] LIKE '[A-Z][A-Z][A-Z][A-Z]-[0-9][0-9][0-9][0-9]')
                                    NOT NULL,
    [Name]          varchar(50)     NOT NULL,
    Credits         decimal(3, 1)
        CONSTRAINT CK_Courses_Credits
            CHECK (Credits IN (3, 4.5, 6))
                                   NOT NULL,
    [Hours]         tinyint
        CONSTRAINT CK_Courses_Hours
            CHECK ([Hours] = 60 OR [Hours] = 90 OR [Hours] = 120)
            --     [Hours] IN (60, 90, 120)
                                    NOT NULL,
    Active          bit             NOT NULL,
    Cost            money
        CONSTRAINT CK_Courses_Money
            CHECK (Cost BETWEEN 400.00 AND 1500.00)
        -- A CHECK constraint will ensure that the value passed in
        -- meets the requirements of the constraint.
                                    NOT NULL,
    -- Table-Level constraints are used for anything involving more than
    -- one column, such as Composite Primary Keys or complex CHECK constraints.
    -- It's a good pattern to put table-level constraint AFTER you have done all the
    -- column definitions.
    CONSTRAINT CK_Courses_Credits_Hours
        CHECK ([Hours] IN (60, 90) AND Credits IN (3, 4.5) OR [Hours] = 120 AND Credits = 6)
        --     \       #1        /
        --                             \       #2        /
        --             \            #3          /
        --                                                    \      #4   /
        --                                                                      \     #5  /
        --                                                           \       #6        /
        --                          \                     #7                  /
)

CREATE TABLE StudentCourses
(
    StudentID       int
        CONSTRAINT FK_StudentCourses_Students
            FOREIGN KEY REFERENCES Students(StudentID)
        -- A FOREIGN KEY constraint means that the only values
        -- acceptable for this column must be values that exist
        -- in the referenced table.
                                    NOT NULL,
    CourseNumber    varchar(10)
        CONSTRAINT FK_StudentCourses_Courses -- All constraint names have to be unique
            FOREIGN KEY REFERENCES Courses([Number])
                                    NOT NULL,
    [Year]          smallint
        CONSTRAINT CK_StudentCourses_Year
            CHECK ([Year] > 2010)
            --     NOT [Year] <= 2010
                                    NOT NULL,
    Term            char(3)         NOT NULL,
    FinalMark       tinyint
        CONSTRAINT CK_StudentCourses_FinalMark
            CHECK (FinalMark BETWEEN 0 AND 100)
            --     FinalMark >= 0 AND FinalMark <=100
                                        NULL, -- can be empty
    [Status]        char(1)
        CONSTRAINT CK_StudentCourses_Status
            CHECK ([Status] LIKE '[AWE]')
            --     [Status] = 'A' OR [Status] = 'E' OR [Status] = 'W'
            --     [Status] IN ('A','W','E')
                                    NOT NULL,
    -- Table-Level Constraint - when a constraint involves more than one column
    CONSTRAINT PK_StudentCourse_StudentID_CourseNumber
        PRIMARY KEY (StudentID, CourseNumber)
        -- Composite Primary Key constraint
)
-- Naming convention for constraints:
-- PREFIX_Tablename_ColumnName  <- PK, CK, DF
-- FK_TableName_RelatedTableName
GO -- Finish the table creation as a batch

/* Editing Table Structure AFTER data exists
SELECT  StudentID, GivenName, Surname, DateOfBirth, Enrolled
FROM    Students

SELECT [Number], [Name], Credits, [Hours], Active, Cost
FROM   Courses

SELECT StudentID, CourseNumber, [Year], Term, [Status]
FROM   StudentCourses
*/
-- Modifying Database Table Schemas with ALTER TABLE

-- Consider the fact that there may be data in the table
-- that you are trying to alter.
-- If you don't have a default value to apply when
-- adding your new column, then the new column should
-- allow NULL values.
ALTER TABLE Students
   ADD  Email   varchar(30)     NULL
-- Adding a column
/*
-- Here's a quick'n'dirty way to see all the data

SELECT  * -- * means "all columns" in my table
FROM    Students
*/

-- New requirement: We need a column called "Paid" on
-- the StudentCourses table. This has to be a Required
-- column (NOT NULL).
-- We need a DEFAULT value for existing rows of data.
ALTER TABLE StudentCourses
    ADD     Paid    bit     NOT NULL
        CONSTRAINT DF_StudentCourses_Paid DEFAULT (0)
-- Comment out line(s) of code: 
--   [Ctrl] + [k], [Ctrl] + [c]
-- Un-comment:
--   [Ctrl] + [k], [Ctrl] + [u]
-- SELECT * FROM StudentCourses

-- Practice: Add a column "OverDue" to the 
-- StudentCourses table as a bit. (Optional)
ALTER TABLE StudentCourses
    ADD     OverDue bit     NULL

-- New change request: Have a default for the
-- "OverDue" column: 0
ALTER TABLE StudentCourses
    ADD CONSTRAINT DF_StudentCourses_OverDue
        DEFAULT (0) FOR OverDue
-- Also notice above that I have a slightly different
-- syntax for the default constraint:
--  DEFAULT (value) FOR ColumnName
-- Lastly, not that the new constraint will only apply for
-- new rows of data for my DEFAULT constraint.
/*
-- Testing with inserting some data
INSERT INTO StudentCourses(StudentID, CourseNumber, [Year], Term, [Status])
VALUES (2015, 'DMIT-1508', 2020, 'SEP', 'E')

SELECT * FROM StudentCourses
SELECT * FROM Students
*/

/* ALTER TABLE Statements - PRACTICE */

-- A) Add column to the Courses table called "SyllabusURL" that is a variable-length string of up to 70 characters. Determine for yourself if it should be NULL or NOT NULL.
ALTER TABLE Courses
    ADD SyllabusURL varchar(70) NULL
/* 
SELECT * FROM Courses
INSERT INTO Courses(Number, Name, Credits, Hours, Active, Cost, SyllabusURL)
VALUES ('HACK-0001', 'White-Hat Hacking', 4.5, 90, 1, 450.00, 'gopher://hack.dev')
*/  
-- B) Add a CHECK constraint to the SyllabusURL that will ensure the value matches a website URL (HTTPS://).
ALTER TABLE Courses
        WITH NOCHECK
    --  WITH NOCHECK means it will not apply the CHECK
    --               to the existing data in the table
    ADD CONSTRAINT CK_Courses_SyllabusURL
        CHECK (SyllabusURL LIKE 'https://%')
        --      Match for       'https://DMIT-1508.github.io'
/*
INSERT INTO Courses(Number, Name, Credits, Hours, Active, Cost, SyllabusURL)
VALUES ('HACK-1705', 'Gray-Hat Hacking', 4.5, 90, 1, 450.00, 'https://hack.dev')
INSERT INTO Courses(Number, Name, Credits, Hours, Active, Cost, SyllabusURL)
VALUES ('SHIP-1705', 'Warp Drive Engineering', 4.5, 90, 1, 450.00, 'https://ST.com')
*/

-- C) One of the functions that we can use in SQL is the GETDATE() function that will return the current datetime. Use this GETDATE() function as the default value for new column in Students called "EnrolledDate".
ALTER TABLE Students
    ADD EnrolledDate    datetime    NOT NULL
        CONSTRAINT DF_Students_EnrolledDate
            DEFAULT (GETDATE())
/*
SELECT * FROM Students
INSERT INTO Students(GivenName, Surname, DateOfBirth, Enrolled)
VALUES ('Gary', 'Montgomery', 'Feb 29, 2000', 1)
*/

GO -- end the batch of statements that alter the database

/* CREATE INDEX */

CREATE NONCLUSTERED INDEX IX_Students_Surname
    ON Students(Surname) -- lookup by last name
-- What should we index in our tables?
--  - Foreign Key columns
--  - Anything else that will frequently be use as something we either "lookup by" or "sort by"

CREATE NONCLUSTERED INDEX IX_Students_Surname
    ON Students(StudentID)
CREATE NONCLUSTERED INDEX IX_Students_Surname
    ON Students(CourseNumber)

/* INDEX Statements - Practice*/
-- SELECT * FROM Students
CREATE NONCLUSTERED INDEX IX_Courses_Surname
    ON Courses(Name)
CREATE NONCLUSTERED INDEX IX_StudentCourses_Surname
    ON StudentCourses([Year])

GO
-- Indexes improve the performance of the database when retrieving information. They do this by providing an additional "lookup" table that is sorted by the indexed column(s).

-- When we create a table with a PRIMARY KEY, then that/those column(s) are given "clustered" indexes. In other words, the data in the database will (by default) be "sorted by" the Primary Key column(s).

-- We can add additional columns as indexes for quick lookup, but those have to be as "Non-Clustered" indexes.

CREATE NONCLUSTERED INDEX IX_Students_Surname
    ON Students(Surname) -- lookup by last name


-- What should we index in our tables?
--   - Foreign Key Columns
--   - Anything else that will frequently be used as
--     something we either "lookup by" or "sort by"
CREATE NONCLUSTERED INDEX IX_StudentCourses_StudentID
    ON StudentCourses(StudentID)
CREATE NONCLUSTERED INDEX IX_StudentCourses_CourseNumber
    ON StudentCourses(CourseNumber)


/* INDEX Statements - Practice */

-- A) Add an index for the Name column in the Courses table.
CREATE NONCLUSTERED INDEX IX_Courses_Name
    ON Courses(Name) -- Name is a column name
-- B) Add an index for the Year column in the StudentCourses table.
CREATE NONCLUSTERED INDEX IX_StudentCourses_Year
    ON StudentCourses([Year])
-- SELECT * FROM Students
GO -- End the batch
