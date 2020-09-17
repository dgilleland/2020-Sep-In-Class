using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WestWindConsole.Entities
{
    public class Customer
    {
        [Key, Required, StringLength(5, MinimumLength = 5)]
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string ContactEmail { get; set; }
        public int AddressID { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        #region Navigation Properties
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        #endregion
    }
}
