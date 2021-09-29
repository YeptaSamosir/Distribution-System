using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;

namespace API.Repository.Data
{
    public class StatusRepository : GenericRepository<MyContext, Status, string>
    {
        public StatusRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}