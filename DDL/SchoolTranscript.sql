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
    CREATE DATABASE [SchoolTranscript]
END
GO

-- Switch execution context to the database
USE [SchoolTranscript] -- remaining SQL statements will run against the SchoolTranscript database
GO

-- Drop Tables...
--    Drop them in the REVERSE ORDER I created them
IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'StudentCourses')
    DROP TABLE StudentCourses
IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Courses')
    DROP TABLE Courses
IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Students')
    DROP TABLE Students

-- Create Tables...
--    Build them in an order that supports FK constraints
-- My personal coding convention is to keep all SQL keywords in UPPER CASE except for data types (lower case)
-- and all object names (Tables, ColumnNames, etc.) as TitleCase.
-- A Table Definition consists of a comma-separated list
-- of Column Definitions and Table Constraints
CREATE TABLE Students -- The default "schema" name is [dbo] - a "schema" is a subset of a database
(
    -- Our column definitions will describe the
    -- name of the column and its data type as well as
    -- any "contraints" or "restrictions" around the
    -- data that can be stored in that column
    StudentID       int
        CONSTRAINT PK_Students_StudentID PRIMARY KEY
        -- A PRIMARY KEY constraint prevents duplicate data
                                         IDENTITY (2000, 5)
        -- The IDENTITY constraint does not need a name.
        -- An IDENTITY constraint means that the database server
        -- will take responsibility to put a value in this column
        -- every time a new row is added to the table.
        -- IDENTITY constraints can only be applied to PRIMARY KEY
        -- columns that are of a whole-number numeric type.
        -- The IDENTITY constraint takes two values
        --   - The "seed" or starting value for the first row
        --   - the "increment" or value by which we increment
                                NOT NULL,
    --                          NOT NULL means required
    GivenName       varchar(50)
        CONSTRAINT CK_Students_GivenName
            CHECK (GivenName LIKE '[A-Z][A-Z]%') -- Matches 'Dan' or 'Danny'
                                NOT NULL,
    Surname         varchar(50)
        CONSTRAINT CK_Students_Surname
            CHECK (Surname LIKE '__%') -- Not as good as [A-Z][A-Z]%
                                       -- Silly matches: 42
                                NOT NULL,
    DateOfBirth     datetime    NOT NULL,
    Enrolled        bit         NOT NULL
        CONSTRAINT DF_Students_Enrolled DEFAULT (1)
        -- A DEFAULT constraint means that if no data is supplied
        -- for this column, it will automatically use the default value
)

CREATE TABLE Courses
(
    [Number]        varchar(10)
        CONSTRAINT PK_Courses_Number PRIMARY KEY
        CONSTRAINT CK_Courses_Number
            CHECK ([Number] LIKE '[A-Z][A-Z][A-Z][A-Z]-[0-9][0-9][0-9][0-9]')
                                    NOT NULL,
    [Name]          varchar(50)     NOT NULL,
    Credits         decimal(3,1)
        CONSTRAINT CK_Courses_Credits
            CHECK (Credits IN (3, 4.5, 6))
                                    NOT NULL,
    [Hours]         tinyint
        CONSTRAINT CK_Courses_Hours
            CHECK ([Hours] = 60 OR [Hours] = 90 OR [Hours] = 120)
        -- A CHECK constraint will ensure that the value passed in
        -- meets the requirements of the constraint.
                                    NOT NULL,
    Active          bit             NOT NULL,
    Cost            money
        CONSTRAINT CK_Courses_Cost
            CHECK (Cost BETWEEN 400.00 AND 1500.00)
                                    NOT NULL,
    -- Table-level constraints are used for anything involving more than
    -- one column, such as Composite Primary Keys or complex CHECK constraints.
    -- It's a good pattern to put table-level constraints AFTER you have done all the
    -- column definitions.
    CONSTRAINT CK_Courses_Credits_Hours
        CHECK ([Hours] IN (60,90) AND Credits IN (3, 4.5) OR [Hours] = 120 AND Credits = 6)
        --     \       #1       /
        --                            \      #2         /
        --             \          #3           /
        --                                                   \     #4     /
        --                                                                     \    #5    /
        --                                                         \        #6        /
        --                        \                       #7                  /
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
        CONSTRAINT FK_StudentCourses_Courses -- All constraint names must be unique
            FOREIGN KEY REFERENCES Courses([Number])
                                    NOT NULL,
    [Year]          int
        CONSTRAINT CK_StudentCourses_Year
            CHECK ([Year] > 2010)
            --     NOT [Year] <= 2010
                                    NOT NULL,
    Term            char(3)         NOT NULL,
    FinalMark       tinyint
        CONSTRAINT CK_StudentCourses_FinalMark
            CHECK (FinalMark BETWEEN 0 AND 100)
            --     FinalMark >= 0 AND FinalMark <=100
                                        NULL,
    [Status]        char(1)
        CONSTRAINT CK_StudentCourses_Status
            CHECK ([Status] LIKE '[AWE]')
            --     [Status] = 'A' OR [Status] = 'E' OR [Status] = 'W'
            --     [Status] IN ('A','W','E')
                                    NOT NULL,
    -- Table-Level Constraint - when a constraint involves more than one column
    CONSTRAINT PK_StudentCourses_StudentID_CourseNumber
        PRIMARY KEY (StudentID, CourseNumber)
        -- Composite Primary Key Constraint
)
GO -- Finish the table creation as a batch

-- Modifying Database Table Schemas with ALTER TABLE

-- Consider the fact that there may be data in the table
-- that you are trying to alter.
-- If you don't have a default value to apply when
-- adding your new column, then the new column should
-- allow NULL values.
ALTER TABLE Students
   ADD  Email   varchar(30)     NULL

/* Let's see what our table's data/structure looks like
-- Here's a quick'n'dirty way to see all the data

SELECT  *   -- * means "all columns" in my table
FROM    Students

*/
-- If you include a default with your new column,
-- you can make that column as Required (NOT NULL).
ALTER TABLE StudentCourses
    ADD     Paid    bit     NOT NULL
        CONSTRAINT DF_StudentCourses DEFAULT (0)

-- Comment out line(s) of code[Ctrl] + [k], [Ctrl] + [c]
-- SELECT * FROM StudentCourses

ALTER TABLE StudentCourses
    ADD  OverDue    bit     NULL

-- If I just want to add a constraint, such as a default
-- to an existing column (say, my OverDue column),
-- I can adjust the table for only the constraint
ALTER TABLE StudentCourses
    ADD CONSTRAINT DF_StudentCourses_OverDue
        DEFAULT (0) FOR OverDue
-- Also notice above that I have a slightly different
-- syntax for the default constraint:
--  DEFAULT (value) FOR ColumnName
-- Lastly, note that the new constraint will only apply
-- for new rows of data for my DEFAULT constraint.

/*
-- Testing with inserting some data
INSERT INTO StudentCourses(StudentID, CourseNumber, [Year], Term, [Status])
VALUES  (2015, 'DMIT-1508', 2020, 'SEP', 'E')

SELECT * FROM StudentCourses
*/