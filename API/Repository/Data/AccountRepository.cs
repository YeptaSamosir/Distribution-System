using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using API.Hash;
using API.Models.ViewModels;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;
using API.Config;
using Microsoft.Extensions.Options;

namespace API.Repository.Data
{
    public class AccountRepository : GenericRepository<MyContext, Account, int>
    {
        private readonly MyContext myContext;
        private readonly DbSet<Account> dbSet;
        private readonly MyConfiguration myConfiguration;
        public AccountRepository(MyContext myContext, IOptions<MyConfiguration> myConfiguration) : base(myContext)
        {
            this.myContext = myContext;
            dbSet = myContext.Set<Account>();
            this.myConfiguration = myConfiguration.Value;
        }

        internal int Register(AccountRegisterVM accountRegisterVM)
        {
                DateTime dateNow = DateTime.Now;
                Account account = new Account(accountRegisterVM.Name, Hashing.HashPassword(accountRegisterVM.Password), accountRegisterVM.Username, accountRegisterVM.Email, true, dateNow, dateNow);
                myContext.Accounts.Add(account);
                myContext.SaveChanges();

                foreach (string role in accountRegisterVM.Roles)
                {

                    myContext.AccountRoles.Add(new AccountRole()
                    {
                        RoleId = role,
                        AccountId = account.AccountId,
                    });
                    myContext.SaveChanges();

                }

                return myContext.SaveChanges();
         }

        internal Account FindUsernameOrEmail(string username)
        {
            return myContext.Accounts.Where(x => x.Username.Equals(username) || x.Email.Equals(username)).FirstOrDefault();
        }

        internal Account FindUsername(string username)
        {
            return myContext.Accounts.Where(x => x.Username.Equals(username)).FirstOrDefault();
        }

        internal IEnumerable<RoleVM> getRole(int AccountId)
        {
            return myContext.AccountRoles
            .Where(x => x.AccountId.Equals(AccountId))
            .Join(myContext.Roles, AccountRole => AccountRole.RoleId, Role => Role.RoleId, (AccountRole, Role) => new RoleVM
            {
                AccountId = AccountRole.AccountId,
                Role = Role.Name
            }).ToList();
        }

        internal int DeactivateLoginAccount(Account accountData)
        {
            try
            {
                accountData.IsActive = false;
                Update(accountData);
                myContext.SaveChanges();
                return 1;
            }
            catch {
                throw new Exception();
            }
            
            
        }

        internal string ValidationUnique(string username, string email)
        {
            if (myContext.Accounts.Where(x => x.Email == email).Count() > 0)
            {
                return "Email sudah digunakan";
            }
            if (myContext.Accounts.Where(x => x.Username == username).Count() > 0)
            {
                return "Username sudah digunakan";
            }

            return null;
        }

        internal int ChangePassword(ChangePassword changePassword)
        {
            try
            {
                var emailCheck = myContext.Accounts.SingleOrDefault(x => x.Email.Equals(changePassword.Email));
                if (emailCheck == null)
                {
                    return 0;
                }
                var passwordCheck = myContext.Accounts.SingleOrDefault(x => x.AccountId.Equals(emailCheck.AccountId));
                if (Hashing.ValidatePassword(changePassword.CurrentPassword, passwordCheck.Password))
                {
                    var account = myContext.Accounts.Where(n => n.AccountId == emailCheck.AccountId).FirstOrDefault();
                    account.Password = BCrypt.Net.BCrypt.HashPassword(changePassword.NewPassword);
                    Update(account);
                    myContext.SaveChanges();
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            catch
            {
                throw new Exception();
            }
        }

        internal void Email(string stringHtmlMessage, string destinationEmail, string fromMail, string fromPassword)
        {

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.To.Add(new MailAddress(destinationEmail));
            message.Subject = "Reset Password From Distribution System" + DateTime.Now;
            message.IsBodyHtml = true;
            message.Body = "<html><body> " + stringHtmlMessage + " </body></html>";


            var smtpClient = new SmtpClient(myConfiguration.SmtpServer)
            {
                Port = myConfiguration.Port,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);

        }

        internal int UpdateAccount(AccountRegisterVM accountRegisterVM)
        {

            //update entity account
            var account = myContext.Accounts.Where(x => x.AccountId == accountRegisterVM.AccountId).FirstOrDefault();
            account.Name = accountRegisterVM.Name;
            account.Username = accountRegisterVM.Username;
            account.Email = accountRegisterVM.Email;
            account.IsActive = accountRegisterVM.IsActive;
            account.UpdatedAt = DateTime.Now;
            Update(account);
            myContext.SaveChanges();

            var roleAccount =  myContext.AccountRoles.Where(x => x.AccountId == account.AccountId);
            foreach (var role in roleAccount)
            {
                //delete entity accountrole
                myContext.AccountRoles.Remove(role);
                myContext.SaveChanges();
            }

            //update entity accountrole
            foreach (string role in accountRegisterVM.Roles)
            {
                myContext.AccountRoles.Add(new AccountRole()
                {
                    RoleId = role,
                    AccountId = account.AccountId,
                });
                myContext.SaveChanges();
            }
            return myContext.SaveChanges();
            
        }

        internal bool ForgotPassword(ForgotPassword forgetPassword)
        {
            var emailCheck = myContext.Accounts.Where(x => x.Email.Equals(forgetPassword.Email)).FirstOrDefault();

            //if email exist
            if (emailCheck != null)
            {

                //generate ResetPassword
                string resetPassword = Helper.Helper.GetRandomAlphanumericString(5);
                string stringHtmlMessage = $"Password Baru Anda: <b> {resetPassword} </b> <br> Gunakan password reset ini untuk login";
                // update database
                var checkEmail = myContext.Accounts.SingleOrDefault(x => x.Email.Equals(emailCheck.Email));
                checkEmail.Password = Hashing.HashPassword(resetPassword);
                Update(checkEmail);

                Email(stringHtmlMessage, forgetPassword.Email, myConfiguration.Email, myConfiguration.Password);

                return true;
            }
            return false;
        }
    }

}