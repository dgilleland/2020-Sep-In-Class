using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindTraders.DataStore.Entities
{
    [Table("Shippers")]
    public class Shipper
    {
        #region Column Mappings
        [Key]
        public int ShipperID { get; set; }
        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }
        [StringLength(24)]
        public string Phone { get; set; }
        #endregion

        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}