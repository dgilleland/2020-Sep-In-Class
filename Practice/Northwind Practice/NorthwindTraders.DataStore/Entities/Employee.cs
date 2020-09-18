using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindTraders.DataStore.Entities
{
    [Table("Employees")]
    public class Employee
    {
        #region Column Mappings
        [Key]
        public int EmployeeID { get; set; }
        [Required]
        [StringLength(10)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20)]
        public string Lastname { get; set; }
        [StringLength(30)]
        public string Title { get; set; }
        [StringLength(25)]
        public string TitleOfCourtesy { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
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
        public string HomePhone { get; set; }
        [StringLength(4)]
        public string Extension { get; set; }
        public byte[] Photo { get; set; }
        [StringLength(40)]
        public string PhotoMimeType { get; set; }
        [Column(TypeName = "ntext")]
        public string Notes { get; set; }
        public int? ReportsTo { get; set; }
        [StringLength(255)]
        public string PhotoPath { get; set; }
        public DateTime LastModified { get; set; } = DateTime.Now;

        [NotMapped]
        public string FullName { get { return $"{FirstName} {Lastname}"; } }
        [NotMapped]
        public string FormalName { get { return $"{Lastname}, {FirstName}"; } }
        #endregion
        public virtual ICollection<Employee> Subordinates { get; set; }
        [ForeignKey(nameof(ReportsTo))]
        public virtual Employee Manager { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();

        public virtual ICollection<Territory> Territories { get; set; } = new HashSet<Territory>();
    }
}
