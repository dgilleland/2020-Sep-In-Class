using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace WestWindConsole.Entities
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public int SalesRepID { get; set; }
        [Required, StringLength(5, MinimumLength = 5)]
        public string CustomerID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public decimal? Freight { get; set; }
        public bool Shipped { get; set; }
        public string ShipName { get; set; }
        public int? ShipAddressID { get; set; }
        public string Comments { get; set; }

        #region Navigation Properties
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new HashSet<OrderDetail>();
        public virtual Customer Customer { get; set; }
        #endregion
    }
}
