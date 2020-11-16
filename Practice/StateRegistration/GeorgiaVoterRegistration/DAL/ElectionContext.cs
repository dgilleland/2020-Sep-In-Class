using GeorgiaVoterRegistration.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeorgiaVoterRegistration.DAL
{
    public class ElectionContext : DbContext
    {
        public ElectionContext()
            : base("name=GAdb")
        {

        }

        public DbSet<Voter> Voters { get; set; }
    }
}
