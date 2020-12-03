namespace HigherEd.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class EvaluationComponent
    {
        public int EvaluationComponentId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public byte Weight { get; set; }

        public bool RequiredPass { get; set; }

        public int? EvaluationGroupId { get; set; }

        public int CourseId { get; set; }

        public virtual Course Cours { get; set; }

        public virtual EvaluationGroup EvaluationGroup { get; set; }
    }
}
