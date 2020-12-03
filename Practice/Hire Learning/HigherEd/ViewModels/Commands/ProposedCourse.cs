using System;
using System.Linq;

namespace HigherEd.ViewModels
{
    namespace Commands
    {
        public class ProposedCourse
        {
            public string Number { get; set; }
            public string Name { get; set; }
            public byte Hours { get; set; }
            public decimal Credits { get; set; }
            public byte Term { get; set; }
        }
    }
}
