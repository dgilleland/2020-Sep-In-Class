/* Purpose of my DDL Script file:
- Generate a database with the appropriate tables from my design.
  - Write it in a way that there will be NO unexpected errors when executing. That should include
    - Running it for the first time against a database server
    - Running it again (and again....)

    DROP DATABASE [MyFirstDatabase]
*/
-- Single-line comment begins with two dashes
--     EXISTS() is a function that checks to see if any rows/results came back
--           a SELECT statement looks for information in some location on my Database Server
IF NOT EXISTS (SELECT [name] -- SELECT clause [name] is a column name
               --    master         is the database name
               --            .sys   is the "schema" (subsection of the database)
               --                 .databases   is the name of the Table
               FROM [master].[sys].databases -- FROM clause says "where to look"
               -- WHERE clause filters out the results of my query (SELECT)
               WHERE [name] = N'MyFirstDatabase')
BEGIN -- BLOCK of code that runs IF (TRUE)
    CREATE DATABASE [MyFirstDatabase] -- Square brackets are optional
END
GO -- End of a "batch" of SQL statement(s)

USE [MyFirstDatabase] -- Switch to a specific database for the remainder of this script file
GO -- End of another "batch"

-- The remainder of my script file will now run against the [MyFirstDatabase] database
IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'SimpleTable')
BEGIN -- Use a BEGIN/END block if there are multiple statements you want to execute inside an IF
    DROP TABLE SimpleTable
END

-- When I re-run this script over again, I want the table created as follows:
CREATE TABLE SimpleTable
(
    MyColumn int
)
