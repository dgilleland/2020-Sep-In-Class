#region Using statements (for libraries)
using Microsoft.EntityFrameworkCore;
using static System.Console;
using System;
//using System.Linq;
#endregion

#region Main method
WriteLine("Hello World!");
var db = new SchoolDbContext();
string partialName = args.Length > 0 ? args[0] : "NA";
try
{
    //var result = db.SP_FindStudentClubs.FromSqlInterpolated($"EXEC FindStudentClubs {partialName}");
    var result = db.SP_FindStudentClubs.FromSqlRaw("EXEC FindStudentClubs {0}", partialName);

    foreach (var row in result)
        WriteLine(row);
}
catch (Exception ex)
{
    WriteLine(ex.Message);
}
#endregion

#region Backend
// Class representing data coming from the stored procedure
[Keyless]
public record ClubInfo
{
    public string ClubId { get; set; }
    public string ClubName { get; set; }
}

public class SchoolDbContext : DbContext
{
    // Parameterless Constructor
    public SchoolDbContext() : base()
    { }
    // Property
    public DbSet<ClubInfo> SP_FindStudentClubs { get; set; }

    // Override base method to say where the database is
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server=.;database=A01-School;trusted_connection=true;");
    }

    // Override base method to say how we are modelling the Properties of this class and mapping them to our Db.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<ClubInfo>(eb =>
            {
                eb.HasNoKey();
            });
    }
}
#endregion