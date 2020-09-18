using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindTraders.DataStore.Entities
{
    // It's a "best practice" to name your classes in the singular form
    [Table("Categories")]
    public class Category
    {
        // A property for each column in the DB table
        [Key] // "Annotation" - Primary Key
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "CategoryName is required")]
        [StringLength(15, ErrorMessage = "A category name cannot be longer than 15 characters")]
        public string CategoryName { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        [StringLength(40, ErrorMessage = "The PictureMimeType cannot be longer than 40 characters")]
        public string PictureMimeType { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
