using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;

namespace API.Repository.Data
{
    public class CompanyRepository : GenericRepository<MyContext, Company, int>
    {
        public CompanyRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}