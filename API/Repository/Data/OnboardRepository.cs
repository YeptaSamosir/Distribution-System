using System;
using System.Collections.Generic;
using System.IO;
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
            onboard.DateEnd = DateTime.MinValue;
            myContext.Onboards.Add(onboard);
            myContext.SaveChanges();


            //update status candidate to Onboard
            var candidateData = myContext.Candidates.Where(x => x.CandidateId == onboard.CandidateId).FirstOrDefault();
            candidateData.Status = status.Onboard;
            candidateData.UpdatedAt = DateTime.Now;
            myContext.Candidates.Update(candidateData);
            myContext.SaveChanges();

            //update statud at schedulinterview
            var scheduleInterviewsData = myContext.ScheduleInterviews.Where(x => x.CandidateId == onboard.CandidateId || x.CompanyId == onboard.CompanyId).FirstOrDefault();
            scheduleInterviewsData.StatusId = "ITV-DN"; //interview done
            scheduleInterviewsData.UpdatedAt = DateTime.Now;
            myContext.ScheduleInterviews.Update(scheduleInterviewsData);
            myContext.SaveChanges();

            //get data company 
            var companyData = myContext.Companies.Where(x => x.CompanyId == onboard.CompanyId).FirstOrDefault();

           

            //send email notification to candidate and link calendar
            string dateStart = onboard.DateStart.ToString("dddd, dd MMMM yyyy");
          
            string subjectMail = "[INFO] Schedule Onboard";
            string bodyMail =
                $"Dear {candidateData.Name} <br><br> " +
                $"Your onboard information: <br>" +
                $"Candidate : <b>{candidateData.Name}</b><br>" +
                $"Company : <b>{companyData.Name}</b><br>" +
                $"Postion :  <b>{onboard.JobTitle}</b><br>" +
                $"Date Start : <b>{dateStart}</b><br><br>" +
              
                $"[Distribution System]";

            //Fetching Email Body Text from EmailTemplate File.  
            string FilePath = @"..\Client\wwwroot\assets\email_template\infoscheduleonboard.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();

            string title = "Info Schedule Onboard";
            //Repalce dinamic text  
            MailText = MailText.Replace("[title]", title)
                .Replace("[recipientName]", candidateData.Name)
                .Replace("[companyName]", companyData.Name)
                .Replace("[jobTitle]", onboard.JobTitle)
                .Replace("[candidateName]", candidateData.Name)
                .Replace("[dateStart]", dateStart)
                ;

            MailHelper mailHelper = new MailHelper();
            mailHelper.SmtpClient(
                subjectMail,
                MailText,
                candidateData.Email,
                myConfiguration.From,
                myConfiguration.Email,
                myConfiguration.Password,
                myConfiguration.SmtpServer,
                myConfiguration.Port
            );



            return 1;

        }

        internal int UpdateOnBoard(Onboard onboard)
        {
         
            //UPDATE if onboard done
            if (onboard.StatusId == "ONB-DN")
            {
                //update problem tolong dibenerin
                onboard.DateEnd = DateTime.Now;
                Update(onboard);
                myContext.SaveChanges();


                var candidateData = myContext.Candidates.Where(x => x.CandidateId == onboard.CandidateId).FirstOrDefault();
                candidateData.Status = status.Idle;
                candidateData.UpdatedAt = DateTime.Now;
                myContext.Candidates.Update(candidateData);
                myContext.SaveChanges();
            }


            return 1;


        }
    }
}