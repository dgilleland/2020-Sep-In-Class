namespace HigherEd.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class Student
    {
        [Key]
        public Guid IdNumber { get; set; }

        [StringLength(50)]
        public string GivenNames { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(36)]
        public string SchoolIdentifier { get; set; }
    }
}
