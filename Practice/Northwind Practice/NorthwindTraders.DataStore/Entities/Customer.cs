using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindTraders.DataStore.Entities
{
    [Table("Customers")]
    public class Customer
    {
        #region Column Mappings
        // When a PK is NOT an Identity, use DatabaseGenerated(DatabaseGeneratedOption.None)
        [Key, StringLength(5), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CustomerID { get; set; }
        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }
        [StringLength(30)]
        public string ContactName { get; set; }
        [StringLength(30)]
        public string ContactTitle { get; set; }
        [StringLength(60)]
        public string Address { get; set; }
        [StringLength(15)]
        public string City { get; set; }
        [StringLength(15)]
        public string Region { get; set; }
        [StringLength(10)]
        public string PostalCode { get; set; }
        [StringLength(15)]
        public string Country { get; set; }
        [StringLength(24)]
        public string Phone { get; set; }
        [StringLength(24)]
        public string Fax { get; set; }
        [Column(TypeName = "xml")]
        public string Demographics { get; set; }
        public DateTime LastModified { get; set; }
        #endregion

        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
