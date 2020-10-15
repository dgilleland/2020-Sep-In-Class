# SchoolTranscript Schema

The following information represents the tables and columns in the **SchoolTranscript** database.

<style>
    td, td { border: solid thin black;}
</style>

## Courses

| Column Name | Data Type     | Primary Key | Foreign Key | Nullable | Identity | Default |
|-------------|---------------|-------------|-------------|----------|----------|---------|
| Number      | varchar(10)   | PK          |             |          |          |         |
| Name        | varchar(50)   |             |             |          |          |         |
| Credits     | decimal(3, 1) |             |             |          |          |         |
| Hours       | tinyint       |             |             |          |          |         |
| Active      | bit           |             |             |          |          |         |
| Cost        | money         |             |             |          |          |         |

## StudentCourses

| Column Name  | Data Type   | Primary Key | Foreign Key        | Nullable | Identity | Default |
|--------------|-------------|-------------|--------------------|----------|----------|---------|
| StudentID    | int         | PK          | Students.StudentID |          |          |         |
| CourseNumber | varchar(10) | PK          | Courses.Number     |          |          |         |
| Year         | smallint    |             |                    |          |          |         |
| Term         | char(3)     |             |                    |          |          |         |
| FinalMark    | tinyint     |             |                    | Y        |          |         |
| Status       | char(1)     |             |                    |          |          |         |

## Students

| Column Name | Data Type   | Primary Key | Foreign Key | Nullable | Identity | Default |
|-------------|-------------|-------------|-------------|----------|----------|---------|
| StudentID   | int         | PK          |             |          | Y        |         |
| GivenName   | varchar(50) |             |             |          |          |         |
| Surname     | varchar(50) |             |             |          |          |         |
| DateOfBirth | datetime    |             |             |          |          |         |
| Enrolled    | bit         |             |             |          |          | ((1))   |

