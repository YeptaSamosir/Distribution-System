using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class ScheduleInterviewDateOptionRepository : GenericRepository<MyContext, ScheduleInterviewDateOption, int>
    {
        public ScheduleInterviewDateOptionRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
