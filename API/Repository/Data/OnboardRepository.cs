using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;

namespace API.Repository.Data
{
    public class OnboardRepository : GenericRepository<MyContext, Onboard, int>
    {
        public OnboardRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}