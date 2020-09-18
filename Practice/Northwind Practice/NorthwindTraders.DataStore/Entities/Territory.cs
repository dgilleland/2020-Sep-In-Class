using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindTraders.DataStore.Entities
{
    [Table("Territories")]
    public class Territory
    {
        #region Column Mappings
        [Key]
        [StringLength(20)]
        public string TerritoryID { get; set; }
        [StringLength(50)]
        public string TerritoryDescription { get; set; }
        public int RegionID { get; set; }
        #endregion

        #region Navigation Properties
        public virtual Region Region { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
        #endregion
    }
}
