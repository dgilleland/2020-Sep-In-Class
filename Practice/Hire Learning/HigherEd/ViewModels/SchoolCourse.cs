using System;
using System.Collections.Generic;
using System.Linq;
using Humanizer;
using Humanizer.Inflections;

namespace HigherEd.ViewModels
{
    public class SchoolCourse
    {
        public int CourseId { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public byte Hours { get; set; }
        public decimal Credits { get; set; }
        public byte Term { get; set; }
        public string TermName => ((int)Term).Ordinalize();
        public bool IsElective { get; set; }
        public IEnumerable<CourseReference> Prerequisites { get; set; }
    }
}
