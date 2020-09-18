using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindTraders.DataStore.Entities
{
    [Table("Orders")]
    public class Order
    {
        #region Column Mappings
        [Key]
        public int OrderID { get; set; }
        [StringLength(5)]
        public string CustomerID { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        [Column(TypeName = "money")]
        public decimal? Freight { get; set; }
        [StringLength(40)]
        public string ShipName { get; set; }
        [StringLength(60)]
        public string ShipAddress { get; set; }
        [StringLength(15)]
        public string ShipCity { get; set; }
        [StringLength(15)]
        public string ShipRegion { get; set; }
        [StringLength(10)]
        public string ShipPostalCode { get; set; }
        [StringLength(15)]
        public string ShipCountry { get; set; }

        public DateTime LastModified { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new HashSet<OrderDetail>();

        public virtual Shipper Shipper { get; set; }

        [NotMapped]
        public string OrderInfo
        {
            get
            {
                string text = $"{OrderID} -  {OrderDate?.ToLongDateString()}";
                text = text.EndsWith("-") ? text.Replace("-", "") : text;
                return text;
            }
        }
        #endregion
    }
}
