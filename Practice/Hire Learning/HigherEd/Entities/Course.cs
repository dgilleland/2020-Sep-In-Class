namespace HigherEd.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Courses")]
    internal partial class Course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course()
        {
            CourseOfferings = new HashSet<CourseOffering>();
            EvaluationComponents = new HashSet<EvaluationComponent>();
            Prerequisites = new HashSet<Course>();
            DependentCourses = new HashSet<Course>();
        }

        [Key]
        public int CourseId { get; set; }

        [Required]
        [StringLength(9)]
        public string Number { get; set; }

        [Required]
        [StringLength(70)]
        public string Name { get; set; }

        public byte Hours { get; set; }

        public decimal Credits { get; set; }

        public byte InitialTerm { get; set; }

        public bool IsElective { get; set; }

        public int? ProgramId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseOffering> CourseOfferings { get; set; }

        public virtual ProgramOfStudy ProgramsOfStudy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EvaluationComponent> EvaluationComponents { get; set; }

        /// <summary>
        /// These are pre-requisite courses for this course; the pre-requisite courses must be completed before students can enroll in this course.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course> Prerequisites { get; set; } // Prerequisites

        /// <summary>
        /// These are the courses that depend on this course as a pre-requisite; this course must be completed before anyone can enroll in the <see cref="DependentCourses"/>.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course> DependentCourses { get; set; } // DependentCourses
    }
}
