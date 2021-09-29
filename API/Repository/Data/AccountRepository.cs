using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;

namespace API.Repository.Data
{
    public class AccountRepository : GenericRepository<MyContext, Account, int>
    {
        public AccountRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}