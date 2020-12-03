namespace HigherEd.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class CourseOffering
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CourseOffering()
        {
            CourseLocations = new HashSet<CourseLocation>();
            InstructorAssignments = new HashSet<InstructorAssignment>();
        }

        public int CourseOfferingId { get; set; }

        public int CourseId { get; set; }

        public int AcademicTermId { get; set; }

        [StringLength(5)]
        public string SectionName { get; set; }

        public virtual AcademicTerm AcademicTerm { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseLocation> CourseLocations { get; set; }

        public virtual Course Cours { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstructorAssignment> InstructorAssignments { get; set; }
    }
}
