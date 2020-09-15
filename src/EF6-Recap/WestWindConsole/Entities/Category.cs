using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WestWindConsole.Entities
{
    // TODO: Begin reviewing entity attributes
    // Our entity classes are to provide a mapping between the STRUCTURE of the database tables
    // and the structure of the objects (instances of the class) in our C# application
    [Table("Categories")] // Mapping info about the database table this entity relates to
    public class Category
    {
        [Key] // Identfies that this property/column is a PK column
        public int CategoryID { get; set; }

        // These two attributes provide both mapping and extra validation in EF
        [Required] // Use this for string/varchar columns that are NOT NULL
        [StringLength(15, ErrorMessage = "Category Name cannot be more than 15 characters long")]
        public string CategoryName { get; set; } // C# strings allow for null values

        public string Description { get; set; }

        public byte[] Picture { get; set; }

        [StringLength(40, ErrorMessage = "Picture Mime Type has a maximum of 40 characters")]
        public string PictureMimeType { get; set; }

        // TODO: Introducing Navigation Properties
        // ICollection<T> is a generic interface in C#
        // We use the virtual keyword to allow "lazy loading"
        // Lazy Loading is where we don't automatically load the related data into RAM
        // We use an ICollection<Product> because each Category can apply to many products
        public virtual ICollection<Product> Products { get; set; } =
            new HashSet<Product>(); // HashSet<T> implements the ICollection<T> interface
        // We've given this property a default value -> it's set up as an EMPTY collection
    }
}
