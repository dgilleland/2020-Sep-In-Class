using HigherEd.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace HigherEd.DAL
{
    internal partial class LearningContext : DbContext
    {
        public LearningContext()
            : base("name=LearningContext")
        {
        }

        public virtual DbSet<AcademicTerm> AcademicTerms { get; set; }
        public virtual DbSet<BuildVersion> BuildVersions { get; set; }
        public virtual DbSet<CourseLocation> CourseLocations { get; set; }
        public virtual DbSet<CourseOffering> CourseOfferings { get; set; }
        public virtual DbSet<CourseRegistration> CourseRegistrations { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CreditModel> CreditModels { get; set; }
        public virtual DbSet<EvaluationComponent> EvaluationComponents { get; set; }
        public virtual DbSet<EvaluationGroup> EvaluationGroups { get; set; }
        public virtual DbSet<GovernanceModel> GovernanceModels { get; set; }
        public virtual DbSet<InstructorAssignment> InstructorAssignments { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<ProgramOfStudy> ProgramsOfStudy { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcademicTerm>()
                .Property(e => e.Term)
                .IsUnicode(false);

            modelBuilder.Entity<AcademicTerm>()
                .HasMany(e => e.CourseOfferings)
                .WithRequired(e => e.AcademicTerm)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseLocation>()
                .Property(e => e.RoomNumber)
                .IsUnicode(false);

            modelBuilder.Entity<CourseOffering>()
                .Property(e => e.SectionName)
                .IsUnicode(false);

            modelBuilder.Entity<CourseOffering>()
                .HasMany(e => e.CourseLocations)
                .WithRequired(e => e.CourseOffering)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseOffering>()
                .HasMany(e => e.InstructorAssignments)
                .WithRequired(e => e.CourseOffering)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.Number)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Course>()
                .Property(e => e.Credits)
                .HasPrecision(3, 1);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.CourseOfferings)
                .WithRequired(e => e.Cours)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.EvaluationComponents)
                .WithRequired(e => e.Cours)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Prerequisites)
                .WithMany(e => e.DependentCourses)
                .Map(m => m.ToTable("Prerequisites").MapLeftKey("CourseId").MapRightKey("PrerequisiteCourseId"));

            modelBuilder.Entity<CreditModel>()
                .Property(e => e.Credits)
                .HasPrecision(3, 1);

            modelBuilder.Entity<GovernanceModel>()
                .HasMany(e => e.CreditModels)
                .WithRequired(e => e.GovernanceModel)
                .HasForeignKey(e => e.GovernanceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GovernanceModel>()
                .HasMany(e => e.ProgramsOfStudies)
                .WithOptional(e => e.OperationalPlan)
                .HasForeignKey(e => e.GovernanceModel);

            modelBuilder.Entity<Instructor>()
                .HasMany(e => e.InstructorAssignments)
                .WithRequired(e => e.Instructor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProgramOfStudy>()
                .HasMany(e => e.Courses)
                .WithOptional(e => e.ProgramsOfStudy)
                .HasForeignKey(e => e.ProgramId);
        }
    }
}
