using ChinookTunes.DAL;
using ChinookTunes.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookTunes.BLL
{
    public class SalesController
    {
        public List<Person> ListCustomersByEmailDomain(string emailDomain)
        {
            using (var context = new ChinookContext())
            {
                var result = from row in context.Customers
                             where row.Email.EndsWith(" .com")
                             select new Person
                             {
                                 FirstName = row.FirstName,
                                 SurName = row.LastName;
                             };
                             return result.ToList();
            }
        }
    }
}
