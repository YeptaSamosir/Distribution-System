using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;

namespace API.Repository.Data
{
    public class RoleRepository : GenericRepository<MyContext, Role, string>
    {
        public RoleRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}