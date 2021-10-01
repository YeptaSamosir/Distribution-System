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