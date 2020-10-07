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
-- My personal coding convention is to keep all SQL keywords in UPPER CASE except for data types (lower case)
-- and all object names (Tables, ColumnNames, etc.) as TitleCase.
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
    GivenName       varchar(50) NOT NULL,
    Surname         varchar(50) NOT NULL,
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
                                    NOT NULL,
    [Name]          varchar(50)     NOT NULL,
    Credits         decimal(3, 1)   NOT NULL,
    [Hours]         tinyint         NOT NULL,
    Active          bit             NOT NULL,
    Cost            money
        CONSTRAINT CK_Courses_Money
            CHECK (Cost BETWEEN 400.00 AND 1500.00)
        -- A CHECK constraint will ensure that the value passed in
        -- meets the requirements of the constraint.
                                    NOT NULL
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
    [Year]          smallint        NOT NULL,
    Term            char(3)         NOT NULL,
    FinalMark       tinyint             NULL, -- can be empty
    [Status]        char(1)         NOT NULL,
    -- Table-Level Constraint - when a constraint involves more than one column
    CONSTRAINT PK_StudentCourse_StudentID_CourseNumber
        PRIMARY KEY (StudentID, CourseNumber)
        -- Composite Primary Key constraint
)
