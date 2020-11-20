using FreeCode.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestWindSystem.DAL;
using WestWindSystem.DataModels;
using WestWindSystem.Entities;

namespace WestWindSystem.BLL
{
    [DataObject] // this class provides access to objects that can be DataBound to UI controls
    public class ProductCatalogController
    {
        #region Query Product Catalog
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<ProductCategory> GetProductCatalog()
        {
            using (var context = new WestWindContext())
            {
                // Apply my LinqPad query to this method
                var result = from cat in context.Categories
                             select new ProductCategory
                             {
                                 CategoryName = cat.CategoryName,
                                 Description = cat.Description,
                                 Picture = cat.Picture,
                                 MimeType = cat.PictureMimeType,
                                 Products = from item in cat.Products
                                            select new ProductSummary
                                            {
                                                Name = item.ProductName,
                                                Price = item.UnitPrice,
                                                Quantity = item.QuantityPerUnit
                                            }
                             };
                return result.ToList();
            }
        }
        #endregion

        #region Product Info CRUD Processing
        #region Supplier/Category Supporting Lists
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<KeyValuePair<int, string>> ListSuppliersNameAndId()
        {
            using (var context = new WestWindContext())
            {
                var suppliers = from company in context.Suppliers.ToList()
                                // use .ToList() because KeyValuePair<,> does not translate to SQL
                                select new KeyValuePair<int, string>(company.SupplierID, company.CompanyName);
                return suppliers.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<KeyValuePair<int, string>> ListCategoriesNameAndId()
        {
            using (var context = new WestWindContext())
            {
                // use .ToList() because KeyValuePair<,> does not translate to SQL
                var suppliers = from company in context.Categories.ToList()
                                select new KeyValuePair<int, string>(company.CategoryID, company.CategoryName);
                return suppliers.ToList();
            }
        }
        #endregion

        #region Querying Methods
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<ProductInfo> FilterProducts(string partialName, bool includeDiscontinued)
        {
            using (var context = new WestWindContext())
            {
                var results = from item in context.Products
                              where item.ProductName.Contains(partialName)
                                 && (item.Discontinued == includeDiscontinued || !item.Discontinued)
                              select new ProductInfo
                              {
                                  ProductId = item.ProductID,
                                  Name = item.ProductName,
                                  Price = item.UnitPrice,
                                  QtyPerUnit = item.QuantityPerUnit,
                                  Supplier = item.Supplier.CompanyName,
                                  Category = item.Category.CategoryName,
                                  SupplierId = item.SupplierID,
                                  CategoryId = item.CategoryID,
                                  IsDiscontinued = item.Discontinued
                              };
                return results.ToList();
            }
        }
        #endregion

        #region Command Methods
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void AddProductItem(ProductInfo info)
        {
            #region Step 0: Validation
            List<Exception> errors = new List<Exception>(); // Start with an empty list of problems

            if (info == null)
                errors.Add(new ArgumentNullException(nameof(info), $"No {nameof(ProductInfo)} was supplied for adding a new product to the catalog."));
            else // At least I have a ProductInfo object, so I can examine it for problems
            {
                if (string.IsNullOrWhiteSpace(info.Name))
                    errors.Add(new BusinessRuleException<string>("A product name is required", nameof(info.Name), info.Name));
                    //                                  \ type / \   message                 / \ variable name /  \ value /
                if (info.Price <= 0)
                    errors.Add(new ArgumentOutOfRangeException(nameof(info.Price), $"The supplied price of {info.Price} must be greater than zero."));
                if (string.IsNullOrWhiteSpace(info.QtyPerUnit))
                    errors.Add(new BusinessRuleException<string>("A Qty/Unit is required (e.g.: 'Ea' or 'Case')", nameof(info.QtyPerUnit), info.QtyPerUnit));
                if (info.CategoryId == 0)
                    errors.Add(new BusinessRuleException<int>("A valid category code is required", nameof(info.CategoryId), info.CategoryId));
                if (info.SupplierId == 0)
                    errors.Add(new BusinessRuleException<int>("A valid category code is required", nameof(info.SupplierId), info.SupplierId));
            }
            if (errors.Any())
                throw new BusinessRuleCollectionException("Unable to add product to inventory catalog.", errors);
            #endregion

            #region Step 1: Process the request by adding a new Product to the database
            using (var context = new WestWindContext())
            {
                var newItem = new Product // Entity class
                {
                    ProductName = info.Name?.Trim(), // Null Conditional Operator  ?.
                    QuantityPerUnit = info.QtyPerUnit.Trim(),
                    UnitPrice = info.Price,
                    CategoryID = info.CategoryId,
                    SupplierID = info.SupplierId
                };
                context.Products.Add(newItem); // Add it to my database context instance
                // So far, nothing is storesd in the database.
                // Things will only be saved into the database when the .SaveChanges() method is called
                context.SaveChanges(); // This will cause all the validation attributes to be checked
            }
            #endregion
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public void UpdateProductItem(ProductInfo info) // ProductInfo is our POCO class
        {
            // Step 0: Validation
            if (info == null)
                throw new ArgumentNullException(nameof(info), $"No {nameof(ProductInfo)} was supplied for updating an existing product in the catalog.");
            if (info.Price <= 0)
                throw new ArgumentOutOfRangeException(nameof(info.Price), $"The supplied price of {info.Price} must be greater than zero.");

            // Step 1: Process the request by modifying an existing Product in the database
            using (var context = new WestWindContext())
            {
                // Note to Self: When doing an update for an existing item,
                //               do a .Find() or a .Attach() to load up the
                //               item from the database.
                var given = context.Products.Find(info.ProductId);
                // Assuming I will get a null if the product does not exist
                if (given == null)
                    throw new ArgumentException($"The given product id of {info.ProductId} does not exist in the database.", nameof(info.ProductId));

                // Update the found product with the given information
                given.ProductName = info.Name;
                given.UnitPrice = info.Price;
                given.QuantityPerUnit = info.QtyPerUnit;
                given.CategoryID = info.CategoryId;
                given.SupplierID = info.SupplierId;

                // Grab a DbEntityEntry<Product>, with which I can say what's been changed.
                var existing = context.Entry(given); // .Entry() will look for the Product obj with a matching ID
                // Specify which Product properties I've modified, 'cause I don't want to lose
                // the .Discontinued or the .UnitsOnOrder values.
                existing.Property(nameof(given.ProductName)).IsModified = true;
                existing.Property(nameof(given.UnitPrice)).IsModified = true;
                existing.Property(nameof(given.QuantityPerUnit)).IsModified = true;
                existing.Property(nameof(given.CategoryID)).IsModified = true;
                existing.Property(nameof(given.SupplierID)).IsModified = true;

                // Update the database
                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DiscontinueProductItem(ProductInfo info)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info), "No Product Info was supplied for discontinuing.");
            using (var context = new WestWindContext())
            {
                var existing = context.Products.Find(info.ProductId);
                if (existing == null)
                    throw new ArgumentException("The product was not found", nameof(info.ProductId));
                existing.Discontinued = true;
                var entry = context.Entry(existing);
                entry.Property(x => x.Discontinued).IsModified = true;
                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DeleteProductItem(ProductInfo info)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info), "No Product Info was supplied for deletion.");
            using (var context = new WestWindContext())
            {
                var existing = context.Products.Find(info.ProductId);
                if (existing == null)
                    throw new ArgumentException("The product was not found", nameof(info.ProductId));
                context.Products.Remove(existing);
                context.SaveChanges();
            }
        }
        #endregion
        #endregion
    }
}
