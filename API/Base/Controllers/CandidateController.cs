using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repository.Data;

namespace API.Base.Controllers
{
    public class CandidateController : BaseController<Candidate, CandidateRepository, int>
    {
        public CandidateController(CandidateRepository repository) : base(repository)
        {
        }
    }
}