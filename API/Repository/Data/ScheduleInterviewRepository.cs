using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;

namespace API.Repository.Data
{
    public class ScheduleInterviewRepository : GenericRepository<MyContext, ScheduleInterview, string>
    {
        public ScheduleInterviewRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}