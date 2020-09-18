using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindTraders.DataStore.Entities
{
    [Table("Region")]
    public class Region
    {
        #region ColumnMappings
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RegionID { get; set; }
        [Required]
        [StringLength(50)]
        public string RegionDescription { get; set; }
        #endregion
        public virtual ICollection<Territory> Territories { get; set; } = new HashSet<Territory>();
    }
}
