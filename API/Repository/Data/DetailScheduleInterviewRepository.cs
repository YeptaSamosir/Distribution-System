using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;

namespace API.Repository.Data
{
    public class DetailScheduleInterviewRepository : GenericRepository<MyContext, DetailScheduleInterview, int>
    {
        public DetailScheduleInterviewRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}