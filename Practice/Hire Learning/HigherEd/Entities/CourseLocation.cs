namespace HigherEd.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class CourseLocation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseLocationID { get; set; }

        public int CourseOfferingID { get; set; }

        [Required]
        [StringLength(10)]
        public string RoomNumber { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public virtual CourseOffering CourseOffering { get; set; }
    }
}
