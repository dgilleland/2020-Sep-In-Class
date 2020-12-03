namespace HigherEd.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class CreditModel
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GovernanceId { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte Hours { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal Credits { get; set; }

        public virtual GovernanceModel GovernanceModel { get; set; }
    }
}
