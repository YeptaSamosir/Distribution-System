using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Config;
using API.Context;
using API.Helper;
using API.Models;
using API.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace API.Repository.Data
{
    public class ScheduleInterviewRepository : GenericRepository<MyContext, ScheduleInterview, string>
    {
        private readonly MyContext myContext;
        private readonly MyConfiguration myConfiguration;
        public ScheduleInterviewRepository(MyContext myContext, IOptions<MyConfiguration> myConfiguration) : base(myContext)
        {
            this.myContext = myContext;
            this.myConfiguration = myConfiguration.Value;
        }

        internal int CreateScheduleInterview(SceduleInterviewVM sceduleInterviewVM)
        {
            try
            {
                //check company id database if null create new company
                int companyId;
                var companyInDatabase = myContext.Companies.Where(x => x.Name.Equals(sceduleInterviewVM.CompanyName)).FirstOrDefault();
                if (companyInDatabase == null)
                {
                    Company company = new Company(sceduleInterviewVM.CompanyName, DateTime.Now, DateTime.Now);
                    myContext.Companies.Add(company);
                    myContext.SaveChanges();

                    companyId = company.CompanyId;
                }
                else {
                    companyId = companyInDatabase.CompanyId;
                }
                //create scheduleInterviewID example "ITV_CP1CD1"
                string ScheduleInterviewId = $"ITV_CP{companyId}CD{sceduleInterviewVM.CandidateId}";
                //add to Entity ScheduleInterview
                ScheduleInterview scheduleInterview = new ScheduleInterview(
                    ScheduleInterviewId,
                    sceduleInterviewVM.CandidateId,
                    companyId,
                    sceduleInterviewVM.CustomerName,
                    sceduleInterviewVM.JobTitle,
                    sceduleInterviewVM.Location, 
                    "ITV-WD",
                    DateTime.Now,
                    DateTime.Now
                );
                myContext.ScheduleInterviews.Add(scheduleInterview);
                myContext.SaveChanges();

                //add to Entity DetailScheduleInterview
                DetailScheduleInterview detailScheduleInterview = new DetailScheduleInterview();
                detailScheduleInterview.ScheduleInterviewId = scheduleInterview.ScheduleInterviewId;
                detailScheduleInterview.EmailCandidate = sceduleInterviewVM.CandidateEmail;
                detailScheduleInterview.EmailCustomer = sceduleInterviewVM.CustomerEmail;
                detailScheduleInterview.TypeLocation = (DetailScheduleInterview.typeLocation)sceduleInterviewVM.Type;
                detailScheduleInterview.CreatedAt = DateTime.Now;
                detailScheduleInterview.UpdatedAt = DateTime.Now;
                myContext.DetailScheduleInterviews.Add(detailScheduleInterview);
                myContext.SaveChanges();

                //add to Entity scheduleInterviewDateOptions
                ScheduleInterviewDateOption scheduleInterviewDateOptionOne = new ScheduleInterviewDateOption(
                    scheduleInterview.ScheduleInterviewId,
                    sceduleInterviewVM.DateTimeOne,
                    DateTime.Now,
                    DateTime.Now
                );
                myContext.ScheduleInterviewDateOptions.Add(scheduleInterviewDateOptionOne);
                myContext.SaveChanges();

                ScheduleInterviewDateOption scheduleInterviewDateOptionTwo = new ScheduleInterviewDateOption(
                    scheduleInterview.ScheduleInterviewId,
                    sceduleInterviewVM.DateTimeTwo,
                    DateTime.Now,
                    DateTime.Now
                );
                myContext.ScheduleInterviewDateOptions.Add(scheduleInterviewDateOptionTwo);
                myContext.SaveChanges();

                ScheduleInterviewDateOption scheduleInterviewDateOptionThree = new ScheduleInterviewDateOption(
                   scheduleInterview.ScheduleInterviewId,
                   sceduleInterviewVM.DateTimeThree,
                   DateTime.Now,
                   DateTime.Now
                );
                myContext.ScheduleInterviewDateOptions.Add(scheduleInterviewDateOptionThree);
                myContext.SaveChanges();




                //getdataCandidate
                var candidateData = myContext.Candidates.Where(x => x.CandidateId.Equals(sceduleInterviewVM.CandidateId)).FirstOrDefault();

                //set sceduledate option
                string mailTo = (sceduleInterviewVM.ScheduleFollowBy == "candidate") ? sceduleInterviewVM.CandidateEmail : sceduleInterviewVM.CustomerEmail;
                string recipientName = (sceduleInterviewVM.ScheduleFollowBy == "candidate") ? candidateData.Name : sceduleInterviewVM.CustomerName;
                string userFollow = (sceduleInterviewVM.ScheduleFollowBy == "candidate") ? "Interviewer" : "Candidate";
               
                string ScheduleDate1 = sceduleInterviewVM.DateTimeOne.ToString("dddd, dd MMMM hh:mm tt");
                string ScheduleDate2 = sceduleInterviewVM.DateTimeTwo.ToString("dddd, dd MMMM hh:mm tt");
                string ScheduleDate3 = sceduleInterviewVM.DateTimeThree.ToString("dddd, dd MMMM hh:mm tt");

                string linkSchedule1 = $"{myConfiguration.BaseUrlClient}/interview/confirmation/{scheduleInterview.ScheduleInterviewId}/{scheduleInterviewDateOptionOne.Id}";
                string linkSchedule2 = $"{myConfiguration.BaseUrlClient}/interview/confirmation/{scheduleInterview.ScheduleInterviewId}/{scheduleInterviewDateOptionTwo.Id}";
                string linkSchedule3 = $"{myConfiguration.BaseUrlClient}/interview/confirmation/{scheduleInterview.ScheduleInterviewId}/{scheduleInterviewDateOptionThree.Id}";

                //checktype
                string location;
                if (detailScheduleInterview.TypeLocation == 0)//Online
                {
                    location = $"Location : <a href={sceduleInterviewVM.Location}> Join </a><br><br>";
                }
                else
                {
                    location = $"Place : {sceduleInterviewVM.Location} <br><br>";
                }

                string subjectMail = "Interview Schedule";
                string bodyMail =
                   
                    $"Dear {recipientName}<br><br> " +
                    $"You will conduct an interview with the following details: <br>" +
                    $"Company : <b>{sceduleInterviewVM.CompanyName}</b><br>" +
                    $"Position : <b>{sceduleInterviewVM.JobTitle}</b><br>" +
                    $"Candidate : <b>{candidateData.Name}</b><br>" +
                    $"User : <b>{sceduleInterviewVM.CustomerName}</b><br>" +
                    $"{location}" +
               
                    $"Please choose a schedule you can, We will contact {userFollow} for follow your schedule : <br>" +
                    $"<a href={linkSchedule1}> {ScheduleDate1}</a><br><br>" +
                    $"<a href={linkSchedule2}> {ScheduleDate2}</a><br><br>" +
                    $"<a href={linkSchedule3}> {ScheduleDate3}</a><br><br>" +
                    $"<br><br>" +
                    $"[Distribution System]";


                MailHelper mailHelper = new MailHelper();
                mailHelper.SmtpClient(
                    subjectMail,
                    bodyMail,
                    mailTo,
                    myConfiguration.From,
                    myConfiguration.Email,
                    myConfiguration.Password,
                    myConfiguration.SmtpServer,
                    myConfiguration.Port
                );
                return 1;
            }
            catch (Exception)
            {

                throw;
            }

        }

        internal string ConfirmDateScheduleInterview(InterviewResponseVM interviewResponseVM)
        {
            try
            {
                //get data dateschedulefix
                var dateSceduleFixData = myContext.ScheduleInterviewDateOptions.Where(x => x.Id == interviewResponseVM.ScheduleDateConfirmId).FirstOrDefault();


                //update status schedule to ITV-OG (interview on going) and datetime interview
                var scheduleInterviewData = myContext.ScheduleInterviews.Where(x => x.ScheduleInterviewId == interviewResponseVM.ScheduleInterviewId).FirstOrDefault();
                scheduleInterviewData.StatusId = "ITV-OG";
                scheduleInterviewData.StartInterview = dateSceduleFixData.DateInterview;
                Update(scheduleInterviewData);
                myContext.SaveChanges();

                //get data detailSchedule
                var detailScheduleInterviewData = myContext.DetailScheduleInterviews.Where(x => x.ScheduleInterviewId == scheduleInterviewData.ScheduleInterviewId).FirstOrDefault();


                //checktype
                string location;
                if (detailScheduleInterviewData.TypeLocation == 0)//Online
                {
                    location = $"Location : <a href={scheduleInterviewData.Location}> Join </a><br><br>";
                }
                else {
                    location = $"Place : {scheduleInterviewData.Location} <br><br>";
                }


                //send email schedule to candidate
                string subjectMail1 = "Invitations to interview";
                string bodyMail1 =
                    $"Dear {scheduleInterviewData.Candidate.Name}<br><br> " +
                    $"You will conduct an interview with the following details: <br>" +
                    $"Company : <b>{scheduleInterviewData.Company.Name}</b><br>" +
                    $"Position : <b>{scheduleInterviewData.JobTitle}</b><br>" +
                    $"User : <b>{scheduleInterviewData.CustomerName}</b><br>" +
                    $"Date : <b>{scheduleInterviewData.StartInterview.ToString("dddd, dd MMMM hh:mm tt")}</b><br>" +
                    $"{location}"+
              
                    $"<br><br>" +
                    $"[Distribution System]";


                MailHelper mailToCandidate = new MailHelper();
                mailToCandidate.SmtpClient(
                    subjectMail1,
                    bodyMail1,
                    detailScheduleInterviewData.EmailCandidate,
                    myConfiguration.From,
                    myConfiguration.Email,
                    myConfiguration.Password,
                    myConfiguration.SmtpServer,
                    myConfiguration.Port
                );

                //send email schedule to customer
                string linkAccept = $"{myConfiguration.BaseUrlClient}/interview/{scheduleInterviewData.ScheduleInterviewId}/ACCEPTED";
                string linkCancel = $"{myConfiguration.BaseUrlClient}/interview/{scheduleInterviewData.ScheduleInterviewId}/CANCEL";

                string subjectMail2 = "Invitations to interview";
                string bodyMail2 =
                    $"Dear {scheduleInterviewData.CustomerName}<br><br> " +
                    $"You will conduct an interview with the following details: <br>" +
                    $"Company : <b>{scheduleInterviewData.Company.Name}</b><br>" +
                    $"Position : <b>{scheduleInterviewData.JobTitle}</b><br>" +
                    $"User : <b>{scheduleInterviewData.CustomerName}</b><br>" +
                    $"Date : <b>{scheduleInterviewData.StartInterview.ToString("dddd, dd MMMM hh:mm tt")}</b><br>" +
                    $"{location}" +
                    $"</hr>" +
                    $"After conducting the interview, please confirm the candidate acceptance : <br>" +
                    $"<a href={linkAccept}> candidate accepted </a><br>" +
                    $"<a href={linkCancel}> candidate not accepted </a><br><br>" +

                    $"[Distribution System]";


                MailHelper mailToCustromer = new MailHelper();
                mailToCustromer.SmtpClient(
                    subjectMail2,
                    bodyMail2,
                    detailScheduleInterviewData.EmailCustomer,
                    myConfiguration.From,
                    myConfiguration.Email,
                    myConfiguration.Password,
                    myConfiguration.SmtpServer,
                    myConfiguration.Port
                );
                return "Success! Check your email for update schedule interview";
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }

        internal string GetStatusInterview(string scheduleInterviewId)
        {
            var scheduleData = myContext.ScheduleInterviews.Where(x => x.ScheduleInterviewId.Equals(scheduleInterviewId)).FirstOrDefault();
            return scheduleData.StatusId;
        }
    }
}