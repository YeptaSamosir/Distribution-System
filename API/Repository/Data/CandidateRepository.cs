using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;

namespace API.Repository.Data
{
    public class CandidateRepository : GenericRepository<MyContext, Candidate, int>
    {
        public CandidateRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}