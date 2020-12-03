namespace HigherEd.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class InstructorAssignment
    {
        public int Id { get; set; }

        public Guid InstructorId { get; set; }

        public int CourseOfferingId { get; set; }

        public bool Active { get; set; }

        public virtual CourseOffering CourseOffering { get; set; }

        public virtual Instructor Instructor { get; set; }
    }
}
