namespace HigherEd.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProgramsOfStudy")]
    internal partial class ProgramOfStudy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProgramOfStudy()
        {
            Courses = new HashSet<Course>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(120)]
        public string ProgramName { get; set; }

        public byte? Accreditation { get; set; }

        public bool Active { get; set; }

        public int? GovernanceModel { get; set; }

        public byte? CreditsThreshold { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course> Courses { get; set; }

        public virtual GovernanceModel OperationalPlan { get; set; }
    }
}
