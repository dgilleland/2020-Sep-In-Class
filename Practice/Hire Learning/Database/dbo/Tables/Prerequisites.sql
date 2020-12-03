CREATE TABLE [dbo].[Prerequisites]
(
    [CourseId] INT NOT NULL , 
    [PrerequisiteCourseId] INT NOT NULL, 
    PRIMARY KEY ([CourseId], [PrerequisiteCourseId]), 
    CONSTRAINT [FK_Prerequisites_CourseNumber] FOREIGN KEY ([CourseId]) REFERENCES [Courses]([CourseId]), 
    CONSTRAINT [FK_Prerequisites_PrerequisiteCourseNumber] FOREIGN KEY ([PrerequisiteCourseId]) REFERENCES [Courses]([CourseId])
)
/*
    /// <summary>
    /// A course may have certain dependencies on other courses. Depedent relationships may be "Prerequisite" or "Corequisite". The relationship may be "Required" or "Suggested".
    /// </summary>
    [PrimaryKey("CourseDependencyID")]
    public class CourseDependency
    {
        public int CourseDependencyID { get; set; }
        public int CourseID { get; set; }
        public int DependsOnCourseID { get; set; }
        public string Relationship { get; set; }
        public string Connection { get; set; }

        public enum DependencyRelationship { Unknown, Prerequisite, Corequisite }
        public enum DependencyConnection { Undetermined, Required, Suggested }
    }

*/
