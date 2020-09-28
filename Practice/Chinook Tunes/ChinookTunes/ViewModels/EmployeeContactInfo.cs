using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Classes in the ViewModels namespace are designed for the Presentation Layer's needs.
/// </summary>
namespace ChinookTunes.ViewModels
{    
    public class EmployeeContactInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
    }
}
