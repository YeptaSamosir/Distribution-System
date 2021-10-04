using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using API.Models;
using Client.Repository.Data;

namespace Client.Base.Controllers
{
    public class CandidateController : BaseController<Candidate, CandidateRepository, int>
    {
        private readonly CandidateRepository repository;
        public CandidateController(CandidateRepository repository) : base(repository)
        {
            this.repository = repository;
        }
    }
}