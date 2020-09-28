using ChinookTunes.DAL;
using ChinookTunes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookTunes
{
    public class ContactController
    {
        public List<EmployeeContactInfo> ListCurrentEmployees()
        {
            using(var context = new ChinookContext())
            {
                // We can use Language INtegrated Query (LINQ) to get data from the database
                // and "reshape it" to meet the needs of our application.
                // LINQ may look a lot like SQL, but it's actually C# code
                var result = from person in context.Employees
                             select new EmployeeContactInfo
                             {
                                 FirstName = person.FirstName,
                                 LastName = person.LastName,
                                 JobTitle = person.Title,
                                 Phone = person.Phone,
                                 Fax = person.Fax,
                                 Email = person.Email
                             };
                return result.ToList();
            }
        }
    }
}
