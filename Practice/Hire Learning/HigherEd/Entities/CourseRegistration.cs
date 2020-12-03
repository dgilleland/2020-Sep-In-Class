namespace HigherEd.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class CourseRegistration
    {
        public int CourseRegistrationId { get; set; }

        public int CourseOfferingId { get; set; }

        public Guid StudentId { get; set; }
    }
}
