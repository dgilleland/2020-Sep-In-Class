/* Purpose of my DDL Script file:
- Generate a database with the appropriate tables from my design.
  - Write it in a way that there will be NO unexpected errors when executing. That should include
    - Running it for the first time
    - Running it again

  USE Master
  DROP DATABASE [MyFirstDatabase]
*/

-- An initial "batch" to create the database
IF NOT EXISTS (SELECT [name]
               FROM master.sys.databases
               WHERE [name] = N'MyFirstDatabase')
BEGIN
    CREATE DATABASE [MyFirstDatabase]
END
GO -- End of a "batch" or set of statements

-- The start of another "batch" of statements
USE [MyFirstDatabase] -- Switch to a specific database for the remainder of this script file
GO

-- The remainder of my script file can run as a single batch
IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'SimplTable')
    DROP TABLE SimplTable -- Get rid of the database table

CREATE TABLE SimplTable
(
    MyColumn int
)
