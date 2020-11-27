using Bogus;
using GeorgiaVoterRegistration.DAL;
using GeorgiaVoterRegistration.Entities;
using GeorgiaVoterRegistration.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeorgiaVoterRegistration.BLL
{
    [DataObject]
    public class DominionController
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<Voter> ListAllVoters()
        {
            using(var context = new ElectionContext())
            {
                return context.Voters.ToList();
            }
        }
        private static Random Rnd = new Random();
        private static Candidate ActualVote => Rnd.Next(10) < 5 ? Candidate.Democrat : Candidate.Republican;
        public VotingMachine LoadTally()
        {
            var result = new VotingMachine();
            using (var context = new ElectionContext())
            {
                var republicans = new List<Ballot>();
                var democrats = new List<Ballot>();
                var voters = context.Voters.ToList();
                while(voters.Any())
                {
                    var first = voters.First();
                    if (ActualVote == Candidate.Democrat)
                        democrats.Add(RecordVote(first, Candidate.Democrat));
                    else
                        republicans.Add(RecordVote(first, Candidate.Republican));
                    voters.Remove(first);
                }
                result.Democrat = democrats;
                result.Republican = republicans;
            }
            return result;
        }

        Ballot RecordVote(Voter first, Candidate candidate)
        {
            return new Ballot
            {
                VoterId = first.VoterId,
                ObfuscatedName = Obfuscate(first.FirstName, first.LastName),
                PresidentialTicket = candidate
            };
        }
        static string Obfuscate(string firstName, string lastName)
        {
            string name = firstName + lastName;
            string result = string.Empty.PadRight(name.Length, '*');
            for (int count = 0; count < 4; count++)
            {
                int index = Rnd.Next(name.Length);
                char reveal = name[index];
                StringBuilder builder = new StringBuilder();
                var firstPart = result.Take(index - 1);
                builder.Append(firstPart.ToArray());
                builder.Append(reveal);
                var lastPart = result.Skip(index).Take(name.Length - index);
                builder.Append(lastPart.ToArray());
                result = builder.ToString();
            }
            return result;
        }
        public void GenerateData()
        {
            // We'll use the Bogus NuGet package to create fake voters.
            var faker = new Faker<Voter>()
                .RuleFor(o => o.FirstName, f => f.Person.FirstName)
                .RuleFor(o => o.LastName, f => f.Person.LastName)
                .RuleFor(o => o.Email, (f, u) => f.Person.Email)
                .RuleFor(o => o.Avatar, f => f.Person.Avatar)
                .RuleFor(o => o.DateOfBirth, f => f.Date.Between(DateTime.Now.AddYears(-60), DateTime.Now.AddYears(-18)));
            var demo = faker.Generate(40); // demo-crat
            using(var context = new ElectionContext())
            {
                foreach(var person in demo)
                {
                    context.Voters.Add(person);
                }
                context.SaveChanges();
            }
        }
    }
}
