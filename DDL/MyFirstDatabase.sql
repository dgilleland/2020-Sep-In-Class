-- Single-line comments starting your comment with two dashes
-- [CTRL] s    - to save the file

-- Because I only need to create my database once,
-- I will "wrap" my CREATE DATABASE in an IF
--     EXISTS () is a function that checks to see if any rows/results came back
IF NOT EXISTS (SELECT [name] -- SELECT statement [name] is a column name
               --   master       is the database name
               --         .sys   is the "schema" (subsection)
               --             .databases   is the name of the table
               FROM master.sys.databases -- FROM says "where to look"
               -- WHERE clause filters out the results of my query (SELECT)
               WHERE [name] = N'MyFirstDatabase') -- strings are in single quotes
BEGIN -- BLOCK of code that runs IF (TRUE)
    CREATE DATABASE [MyFirstDatabase] -- Square Brackets are optional
END
GO

-- Only drop the database if it exists
IF EXISTS (SELECT [name] FROM master.sys.databases WHERE [name] = N'MyFirstDatabase')
BEGIN
-- Delete a database
    DROP DATABASE [MyFirstDatabase]
END
GO
