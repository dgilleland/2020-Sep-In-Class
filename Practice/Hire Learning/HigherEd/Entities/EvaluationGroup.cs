namespace HigherEd.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class EvaluationGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EvaluationGroup()
        {
            EvaluationComponents = new HashSet<EvaluationComponent>();
        }

        public int EvaluationGroupId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public byte Weight { get; set; }

        public bool RequiredPass { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EvaluationComponent> EvaluationComponents { get; set; }
    }
}
