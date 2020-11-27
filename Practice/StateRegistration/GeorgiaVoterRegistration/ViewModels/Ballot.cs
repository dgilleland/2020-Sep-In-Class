using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeorgiaVoterRegistration.ViewModels
{
    public class Ballot
    {
        public int VoterId { get; set; }
        public string ObfuscatedName { get; set; }
        public Candidate PresidentialTicket { get; set; }
    }
    public enum Candidate { Republican, Democrat }
    public class VotingMachine
    {
        public IEnumerable<Ballot> Republican { get; set; }
        public IEnumerable<Ballot> Democrat { get; set; }
    }
}
