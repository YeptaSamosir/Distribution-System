using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;

namespace API.Repository.Data
{
    public class TypeStatusRepository : GenericRepository<MyContext, TypeStatus, string>
    {
        public TypeStatusRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}