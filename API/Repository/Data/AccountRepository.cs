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

        public int ChangePassword(ChangePassword changePassword)
        {
            try
            {
                var emailCheck = myContext.Accounts.SingleOrDefault(x => x.Email.Equals(changePassword.Email));
                if (emailCheck == null)
                {
                    return 3;
                }
                else
                {
                    var passwordCheck = myContext.Accounts.SingleOrDefault(x => x.AccountId.Equals(emailCheck.AccountId));
                    if (emailCheck != null)
                    {
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
                            return 0;
                        }
                    }
                    else
                    {
                        return 2;
                    }
                }
            }
            catch
            {
                throw new Exception();
            }
        }

        public static void Email(string stringHtmlMessage, string destinationEmail)
        {

            MailMessage message = new MailMessage();
            SmtpClient client = new SmtpClient();
            message.From = new MailAddress("lapungproject@gmail.com");
            message.To.Add(new MailAddress(destinationEmail));
            message.Subject = "Reset Password";
            message.IsBodyHtml = true;
            message.Body = stringHtmlMessage;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("lapungproject@gmail.com", "lapungproject19");
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(message);
        }

        public void ForgotPassword(ForgotPassword forgetPassword)
        {
            var emailCheck = myContext.Accounts.Where(x
                => x.Email.Equals(forgetPassword.Email)).FirstOrDefault();

            //if email exist
            if (emailCheck != null)
            {
                // generate guid
                string guid = Guid.NewGuid().ToString();
                string stringHtmlMessage = $"Password Baru Anda: {guid}";
                string hashPW = Hashing.HashPassword(guid);
                // update database
                var checkEmail = myContext.Accounts.SingleOrDefault(x => x.Email.Equals(emailCheck.Email));
                checkEmail.Password = hashPW;
                Update(checkEmail);

                Email(stringHtmlMessage, forgetPassword.Email);
            }
            else
            {

            }
        }
    }

}