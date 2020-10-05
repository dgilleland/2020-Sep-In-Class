using ChinookTunes.DAL;
using ChinookTunes.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookTunes.BLL
{
    [DataObject]
    public class SalesController
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Person> ListCustomersByEmailDomain(string emailDomain)
        {
            using (var context = new ChinookContext())
            {
                var result =
                    from row in context.Customers
                    where row.Email.EndsWith(emailDomain)
                    select new Person
                    //  new // Anonymous data type
                    // The initializer list determines what properties will exist in the anonymous type
                    {
                        // By default, the property name is "inferred" from the property of the entity
                        //row.FirstName, // .FirstName
                        FirstName = row.FirstName, // Explicitly use the property name as assignment
                        Surname = row.LastName // .Surname
                    };
                return result.ToList();
            }
        }
    }
}
