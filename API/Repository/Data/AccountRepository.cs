using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using API.Hash;
using API.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace API.Repository.Data
{
    public class AccountRepository : GenericRepository<MyContext, Account, int>
    {
        private readonly MyContext myContext;
        private readonly DbSet<Account> dbSet;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
            dbSet = myContext.Set<Account>();
        }

        public int Register(AccountRegisterVM accountRegisterVM)
        {
            var checkUsername = myContext.Accounts.Where(x => x.Username.Equals(accountRegisterVM.Username)).FirstOrDefault();
            var checkEmail = myContext.Accounts.Where(x => x.Email.Equals(accountRegisterVM.Email)).FirstOrDefault();

            if (checkUsername is null && checkEmail is null)
            {
                DateTime dateNow = DateTime.Now;
                Account account = new Account(accountRegisterVM.Name, Hashing.HashPassword(accountRegisterVM.Password), accountRegisterVM.Username, accountRegisterVM.Email, true, dateNow, dateNow);
                myContext.Accounts.Add(account);
                myContext.SaveChanges();


                myContext.AccountRoles.Add(new AccountRole()
                {
                    RoleId = accountRegisterVM.RoleId,
                    AccountId = account.AccountId,
                });
                myContext.SaveChanges();

                return 200;
            }
            else if (checkUsername != null)
            {
                return 201;
            }
            else if (checkEmail != null)
            {
                return 202;
            }
            else
            {
                return 203;
            }
        }

        public IEnumerable<RoleVM> getRole(int AccountId)
        {
            return myContext.AccountRoles
            .Where(x => x.AccountId.Equals(AccountId))
            .Join(myContext.Roles, AccountRole => AccountRole.RoleId, Role => Role.RoleId, (AccountRole, Role) => new RoleVM
            {
                AccountId = AccountRole.AccountId,
                Role = Role.Name
            }).ToList();
        }

        public Account FindUsername(string username)
        {
            var data = myContext.Accounts.Where(x => x.Username.Equals(username)).FirstOrDefault();
            if (data != null)
            {
                return data;
            }
            return null;
        }

        public Account FindEmail(string email)
        {
            var data = myContext.Accounts.Where(x => x.Email.Equals(email)).FirstOrDefault();
            if (data != null)
            {
                return data;
            }
            return null;
        }

        internal bool ChangePassword(int accountId, string passwordReset)
        {
            //reset password
            dbSet.Update(new Account()
            {
                AccountId = accountId,
                Password = BCrypt.Net.BCrypt.HashPassword(passwordReset, BCrypt.Net.BCrypt.GenerateSalt(12))
            });
            myContext.SaveChanges();
            return true;
        }

        internal void Update(Account account)
        {
            throw new NotImplementedException();
        }

        internal void UpdatePassword(Account account)
        {
            throw new NotImplementedException();
        }
    }
}