using HigherEd.DAL;
using HigherEd.Entities;
using HigherEd.ViewModels;
using HigherEd.ViewModels.Commands;
using FreeCode.Exceptions; // for my custom business rule exceptions
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HigherEd.BLL
{
    [DataObject]
    public class CourseCatalogController
    {
        #region Queries
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<SchoolCourse> ListSchoolCourses(int programId)
        {
            /* query Courses and Prerequisites */
            using (var context = new LearningContext())
            {
                var result = from item in context.Courses
                             where item.ProgramId == programId
                             select new SchoolCourse
                             {
                                 CourseId = item.CourseId,
                                 Number = item.Number,
                                 Name = item.Name,
                                 Hours = item.Hours,
                                 Credits = item.Credits,
                                 Term = item.InitialTerm,
                                 IsElective = item.IsElective,
                                 Prerequisites = from required in item.Prerequisites
                                                 select new CourseReference
                                                 {
                                                     CourseId = required.CourseId, //.PrerequisiteCourseId, // the course that is required
                                                     Number = required.Number
                                                 }
                             };
                return result.ToList();
            }
        }


        #region DropDownList Data
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<KeyValueOption<int>> ListSchoolPrograms()
        {
            /* query ProgramsOfStudy */
            using (var context = new LearningContext())
            {
                var result = from program in context.ProgramsOfStudy
                             orderby program.ProgramName
                             select new KeyValueOption<int>
                             {
                                 Key = program.Id,
                                 DisplayText = program.ProgramName
                             };
                return result.ToList();
                throw new NotImplementedException();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<KeyValueOption<int>> ListSchoolCourseSummaries(int programId)
        {
            /* query Courses */
            using (var context = new LearningContext())
            {
                var result = from course in context.Courses
                             where course.ProgramId == programId
                             select new KeyValueOption<int>
                             {
                                 Key = course.CourseId,
                                 DisplayText = course.Number
                             };
                return result.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<KeyValueOption<byte>> ListCourseHours(int programId)
        {
            /* query CreditModels for distinct Hours */
            using (var context = new LearningContext())
            {
                var result = from schoolProgram in context.ProgramsOfStudy             // For the given ProgramOfStudy....
                             where schoolProgram.Id == programId
                             from model in schoolProgram.OperationalPlan.CreditModels  // using its GovernanceModel, get the Hours
                             select model.Hours;
                var data = new List<KeyValueOption<byte>>();
                foreach (var hours in result.Distinct())                               // without duplicates
                    data.Add(new KeyValueOption<byte> { Key = hours, DisplayText = hours.ToString() });
                return data.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<KeyValueOption<decimal>> ListCourseCredits(int programId)
        {
            /* query CreditModels for distinct Credits */
            using (var context = new LearningContext())
            {
                var result = from schoolProgram in context.ProgramsOfStudy             // For the given ProgramOfStudy....
                             where schoolProgram.Id == programId
                             from model in schoolProgram.OperationalPlan.CreditModels  // using its GovernanceModel, get the Hours
                             select model.Credits;
                var data = new List<KeyValueOption<decimal>>();
                foreach (var credit in result.Distinct())                              // without duplicates
                    data.Add(new KeyValueOption<decimal> { Key = credit, DisplayText = credit.ToString() });
                return data.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<KeyValueOption<string>> ListTerms(int programId)
        {
            // HACK: This really needs to be calculated....
            var result = new List<KeyValueOption<string>>();
            result.Add(new KeyValueOption<string> { Key = "1", DisplayText = "1st" });
            result.Add(new KeyValueOption<string> { Key = "2", DisplayText = "2nd" });
            result.Add(new KeyValueOption<string> { Key = "3", DisplayText = "3rd" });
            return result;
            /* query GovernanceModels and ProgramsOfStudy (with math) */
            using (var context = new LearningContext())
            {
                throw new NotImplementedException();
            }
        }
        #endregion
        #endregion

        #region Commands
        /* BUSINESS RULES
        The following business rules must be enforced by the system:
        - The combination of Hours/Credits must exist in the CreditModels for the program's GovernanceModel.
        - Edited courses cannot change the course number (that is fixed when a course is created/added).
        - Course numbers and names must be unique across the whole institution (regardless of the program of study).
        - There can only be a maximum of three pre-requisites per course.
        - Pre-requisite courses must be offered in an initial term that is before the term of the dependent course. For example, the PROG-0221 is in the 2nd term, which means the pre-requisite courses have to be 1st term courses.
        - Term numbers begin at 1 and run up to the number of terms for the whole program. This is determined by a combination of the program's Accreditation and the Term Model governing the program's delivery. For example, given a two-year Diploma offered in a Trimester (2-term/year) model, there will be a total
of four terms: 1st, 2nd, 3rd, and 4th.
            - Term Models are reflected using the following enumeration:
                public enum TermModel { Semester = 1, Trimester, Quarter }
              Both the Semester and Trimester have two terms per year. The Quarter term model has three terms per year. (Summer terms are not counted for the institution.)
            - Accreditation is reflected using the following enumeration:
                public enum Accreditation { None, Certificate, Diploma, AppliedDegree }
              Certificate programs are 1-year; Diploma programs are 2-year, Applied Degrees are 3-year
         * */
        public void AddCourse(int programId, ProposedCourse course)
        {
            /* command affecting Courses */
        }
        
        public void UpdateCourse(int programId, CourseSpecification course)
        {
            /* command affecting Courses and Prerequisites */
            // 0) Validation of input
            var errors = new List<Exception>(); // Empty list of errors

            // 0.1) Standard kind of validation for getting an instance of an object
            if (course == null) // This error is a show-stopper - can't do the work with nothing
                throw new ArgumentNullException(nameof(course), "A Course Specification is required in order to do an update.");

            using(var context = new LearningContext())
            {
                var existingCourse = context.Courses.Find(course.CourseId);
                if(existingCourse == null)
                {
                    // 0.2) The course does not exist
                    errors.Add(new Exception($"The supplied course ({course.CourseId}) does not exist and cannot be updated."));
                }
                else
                {
                    // 0.3) "The combination of Hours/Credits must exist in the CreditModels for the program's GovernanceModel."
                    var creditModels = from row in context.Courses
                                       // \Course/
                                       where row.CourseId == course.CourseId
                                       from model in row.ProgramsOfStudy.OperationalPlan.CreditModels
                                       where model.Hours == course.Hours // Matches the given hours for the course
                                          && model.Credits == course.Credits // and the given credits for the course
                                       select model;
                    if (!creditModels.Any()) // if there are no such combinations of allowed hours/credit
                        errors.Add(new BusinessRuleException<string>(
                            "Invalid combination of hours/credits", // Error Message
                            "course.Hours/course.Credits",          // Parameter properties that are wrong
                            $"{course.Hours}/{course.Credits}"));   // values in those properties

                    // 0.4) Edited courses cannot change the course number
                    if (existingCourse.Number != course.Number)
                        errors.Add(new BusinessRuleException<string>("You cannot change the assigned course number.", // message
                                                                     nameof(course.Number),                           // variable
                                                                     course.Number));                                 // value

                    // 0.5) Course numbers and names must be unique across the whole institution (regardless of the program of study).
                    var otherCourses = from row in context.Courses
                                       where row.CourseId != course.CourseId
                                          && row.Name == course.Name
                                       select row;
                    if (otherCourses.Any()) // if there are other courses with the same name
                        errors.Add(new BusinessRuleException<string>("That course name is in use by another course.", nameof(course.Name), course.Name));

                    // 0.6) There can only be a maximum of three pre-requisites per course
                    if (course.Prerequisites.Count() > 3)
                        errors.Add(new BusinessRuleException<int>("A maximum of 3 pre-requisites are allowed",
                                                                  nameof(course.Prerequisites),
                                                                  course.Prerequisites.Count()));

                    // 0.7) Prerequisite courses must be in a prior term
                    foreach(var requiredCourseId in course.Prerequisites)
                    {
                        var requiredCourse = context.Courses.Find(requiredCourseId);
                        // 0.7a) The course must exist
                        if (requiredCourse == null)
                            errors.Add(new BusinessRuleException<int>($"The required course {requiredCourseId} does not exist",
                                                                      nameof(course.Prerequisites), requiredCourseId));
                        else if (requiredCourse.InitialTerm >= course.Term)
                            errors.Add(new BusinessRuleException<byte>(
                                $"The required course {requiredCourseId} must be before Term {course.Term}",
                                nameof(requiredCourse.InitialTerm), requiredCourse.InitialTerm));
                    }

                    // 0.8) The course's program ID should not change
                    if (existingCourse.ProgramId != programId)
                        errors.Add(new BusinessRuleException<int>("The given course is in a different program.",
                                                                  nameof(programId), programId));
                }


                // 0.end) Report all the errors
                if (errors.Any())
                    throw new BusinessRuleCollectionException("Unable to update the supplied course.", errors);

                // 1) Process the changes in the database
                #region Change db info
                existingCourse.Name = course.Name;
                existingCourse.Hours = course.Hours;
                existingCourse.Credits = course.Credits;
                existingCourse.IsElective = course.IsElective;
                existingCourse.InitialTerm = course.Term;
                // TODO: Whoops - need to change the pre-requisites
                var changes = context.Entry(existingCourse);
                changes.State = System.Data.Entity.EntityState.Modified;
                #endregion

                // 2) Process as a single transaction
                context.SaveChanges();
            } // end of the using(var context ....) statement
        }
        #endregion
    }
}
