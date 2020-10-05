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
IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Courses')
    DROP TABLE Courses
IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Students')
    DROP TABLE Students

-- Create Tables...
--    Build them in an order that supports FK constraints
-- My personal coding convention is to keep all SQL keywords in UPPER CASE except for data types (lower case)
-- and all object names (Tables, ColumnNames, etc.) as TitleCase.
CREATE TABLE Students -- The default "schema" name is [dbo] - a "schema" is a subset of a database
(
    StudentID       int         NOT NULL,
    GivenName       varchar(50) NOT NULL,
    Surname         varchar(50) NOT NULL,
    DateOfBirth     datetime    NOT NULL,
    Enrolled        bit         NOT NULL
)

CREATE TABLE Courses
(
    [Number]        varchar(10)     NOT NULL,
    [Name]          varchar(50)     NOT NULL,
    Credits         decimal(3,1)    NOT NULL,
    [Hours]         tinyint         NOT NULL,
    Active          bit             NOT NULL,
    Cost            money           NOT NULL
)