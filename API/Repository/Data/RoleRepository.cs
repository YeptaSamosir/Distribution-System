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
        private readonly MyContext myContext;
  
        public RoleRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public Role GetRoleId(string roleId)
        {
            return myContext.Roles.Where(x => x.RoleId.Equals(roleId)).FirstOrDefault();
        }
    }
}