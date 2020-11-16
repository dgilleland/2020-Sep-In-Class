using Bogus;
using GeorgiaVoterRegistration.DAL;
using GeorgiaVoterRegistration.Entities;
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

        public void GenerateData()
        {
            // We'll use the Bogus NuGet package to create fake voters.
            var faker = new Faker<Voter>()
                .RuleFor(o => o.FirstName, f => f.Person.FirstName)
                .RuleFor(o => o.LastName, f => f.Person.LastName)
                .RuleFor(o => o.Email, (f, u) => f.Person.Email)
                .RuleFor(o => o.Avatar, f => f.Person.Avatar)
                .RuleFor(o => o.DateOfBirth, f => f.Date.Between(DateTime.Now.AddYears(-60), DateTime.Now.AddYears(-18)));
            var demo = faker.Generate(2000); // demo-crat
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
