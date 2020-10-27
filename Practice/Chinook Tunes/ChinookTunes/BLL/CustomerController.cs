using ChinookTunes.DAL;
using ChinookTunes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookTunes.BLL
{
    public class CustomerController
    {
        public List<SelectionItem> ListAllCustomers()
        {
            using(var context = new ChinookContext())
            {
                var result = from person in context.Customers
                // ![](../../SongListings.png;;0,0,370,34)
                             select new SelectionItem // my ViewModel class for drop-downs
                             {
                                 IDValue = person.CustomerId.ToString(),
                                 DisplayText = person.FirstName + " " + person.LastName
                             };
                return result.ToList();
            }
        }
    }
}
