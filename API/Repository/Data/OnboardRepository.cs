using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Config;
using API.Context;
using API.Helper;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace API.Repository.Data
{
    public class OnboardRepository : GenericRepository<MyContext, Onboard, int>
    {
        private readonly MyContext myContext;
        private readonly DbSet<Onboard> dbSet;
        private readonly MyConfiguration myConfiguration;

        public OnboardRepository(MyContext myContext, IOptions<MyConfiguration> myConfiguration) : base(myContext)
        {
            this.myContext = myContext;
            dbSet = myContext.Set<Onboard>();
            this.myConfiguration = myConfiguration.Value;
        }

        internal int CreateOnboard(Onboard onboard)
        {
            //save to database
            onboard.StatusId = "ONB-OG";
            onboard.CreatedAt = DateTime.Now;
            onboard.UpdatedAt = DateTime.Now;
            myContext.Onboards.Add(onboard);
            myContext.SaveChanges();


            //update status candidate to Onboard
            var candidateData = myContext.Candidates.Where(x => x.CandidateId == onboard.CandidateId).FirstOrDefault();
            candidateData.Status = status.Onboard;
            candidateData.UpdatedAt = DateTime.Now;
            myContext.Candidates.Update(candidateData);
            myContext.SaveChanges();

            //get data company 
            var companyData = myContext.Companies.Where(x => x.CompanyId == onboard.CompanyId).FirstOrDefault();
            

            //send email notification to candidate and link calendar
            string dateStart = onboard.DateStart.ToString("dddd, dd MMMM hh:mm tt");
            string dateEnd = onboard.DateEnd.ToString("dddd, dd MMMM hh:mm tt");
          
            string subjectMail = "[INFO] Schedule Onboard";
            string bodyMail =
                $"Dear {candidateData.Name} <br><br> " +
                $"Your onboard information: <br>" +
                $"Candidate : <b>{candidateData.Name}</b><br>" +
                $"Company : <b>{companyData.Name}</b><br>" +
                $"Date Start : <b>{dateStart}</b><br>" +
                $"Date End : <b>{dateEnd}</b><br>" +
              
                $"[Distribution System]";

            MailHelper mailHelper = new MailHelper();
            mailHelper.SmtpClient(
                subjectMail,
                bodyMail,
                candidateData.Email,
                myConfiguration.From,
                myConfiguration.Email,
                myConfiguration.Password,
                myConfiguration.SmtpServer,
                myConfiguration.Port
            );



            return 1;

        }
    }
}