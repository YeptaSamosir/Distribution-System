using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using API.Hash;
using API.Models.ViewModels;

namespace API.Repository.Data
{
    public class AccountRepository : GenericRepository<MyContext, Account, int>
    {
        private readonly MyContext myContext;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int Login (LoginVM loginVM)
        {
            var usernameCheck = myContext.Accounts.SingleOrDefault(x => x.Username.Equals(loginVM.Username));
            if (usernameCheck == null)
            {
                return 3;
            }
            else
            {
                var passwordCheck = myContext.Accounts.SingleOrDefault(x => x.AccountId.Equals(usernameCheck.AccountId));
                if (usernameCheck != null)
                {
                    if (Hashing.ValidatePassword(loginVM.Password, passwordCheck.Password))
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 2;
                }
            }
        }

        public int Register(AccountRegisterVM accountRegisterVM)
        {
            var checkUsername = myContext.Accounts.Where(x => x.Username.Equals(accountRegisterVM.Username)).FirstOrDefault();
            var checkEmail = myContext.Accounts.Where(x => x.Email.Equals(accountRegisterVM.Email)).FirstOrDefault();

            if (checkUsername is null && checkEmail is null)
            {
                //save enitity account
                DateTime dateNow = DateTime.Now;
                Account account = new Account(accountRegisterVM.Name, Hashing.HashPassword(accountRegisterVM.Password), 
                    accountRegisterVM.Username, accountRegisterVM.Email, true, dateNow, dateNow);
                myContext.Accounts.Add(account);
                myContext.SaveChanges();


                //save enitity accountrole
                myContext.AccountRoles.Add(new AccountRole()
                {
                    RoleId = accountRegisterVM.RoleId,
                    AccountId = account.AccountId,
                });
                
                return myContext.SaveChanges();
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

        public string GetUsername(string username)
        {
            var checkUsername = myContext.Accounts.Where(x => x.Username.Equals(username)).FirstOrDefault();
            if (checkUsername == null)
            {
                return null;
            }
            else
            {
                return checkUsername.Username;
            }
        }

        public string[] GetRole(int id)
        {
            var getRole = (from p in myContext.Accounts
                           where p.AccountId == id
                           join a in myContext.AccountRoles
                           on p.AccountId equals a.AccountId
                           join r in myContext.Roles
                           on a.RoleId equals r.RoleId
                           select new Role
                           {
                               Name = r.Name
                           }).ToList();
            string[] roles = new string[getRole.Count];
            for (int i = 0; i < getRole.Count; i++)
            {
                roles[i] = getRole[i].Name;
            }
            return roles;
        }
    }
}