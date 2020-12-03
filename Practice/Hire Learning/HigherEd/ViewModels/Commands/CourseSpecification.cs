using System;
using System.Collections.Generic;
using System.Linq;

namespace HigherEd.ViewModels
{
    namespace Commands
    {
        public class CourseSpecification
        {
            public int CourseId { get; set; }
            public string Number { get; set; }
            public string Name { get; set; }
            public byte Hours { get; set; }
            public decimal Credits { get; set; }
            public byte Term { get; set; }
            public bool IsElective { get; set; }
            public IEnumerable<int> Prerequisites { get; set; }
        }
    }
}
