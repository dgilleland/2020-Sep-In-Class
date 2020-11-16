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
    }
}
