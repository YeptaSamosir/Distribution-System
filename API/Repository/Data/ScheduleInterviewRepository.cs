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

        internal int CreateInterview(CreateInterviewVM createInterviewVM)
        {
            try
            {
                //check company id database if null create new company
                int companyId;
                var companyInDatabase = myContext.Companies.Where(x => x.Name.Equals(createInterviewVM.CompanyName)).FirstOrDefault();
                if (companyInDatabase == null)
                {
                    Company company = new Company(createInterviewVM.CompanyName, DateTime.Now, DateTime.Now);
                    myContext.Companies.Add(company);
                    myContext.SaveChanges();

                    companyId = company.CompanyId;
                }
                else {
                    companyId = companyInDatabase.CompanyId;
                }
                //create scheduleInterviewID example "ITV-CP1CD1"
                string ScheduleInterviewId = $"ITV-CP{companyId}CD{createInterviewVM.CandidateId}";
                //add to Entity ScheduleInterview
                ScheduleInterview scheduleInterview = new ScheduleInterview(
                    ScheduleInterviewId,
                    DateTime.MinValue,
                    createInterviewVM.CandidateId,
                    companyId,
                    createInterviewVM.CustomerName,
                    createInterviewVM.JobTitle,
                    createInterviewVM.Location, 
                    "ITV-WD",
                    DateTime.Now,
                    DateTime.Now
                );
                myContext.ScheduleInterviews.Add(scheduleInterview);
                myContext.SaveChanges();

                //add to Entity DetailScheduleInterview
                DetailScheduleInterview detailScheduleInterview = new DetailScheduleInterview();
                detailScheduleInterview.ScheduleInterviewId = scheduleInterview.ScheduleInterviewId;
                detailScheduleInterview.EmailCandidate = createInterviewVM.CandidateEmail;
                detailScheduleInterview.EmailCustomer = createInterviewVM.CustomerEmail;
                detailScheduleInterview.TypeLocation = (DetailScheduleInterview.typeLocation)createInterviewVM.Type;
                detailScheduleInterview.CreatedAt = DateTime.Now;
                detailScheduleInterview.UpdatedAt = DateTime.Now;
                myContext.DetailScheduleInterviews.Add(detailScheduleInterview);
                myContext.SaveChanges();

                //getdataCandidate
                var candidateData = myContext.Candidates.Where(x => x.CandidateId.Equals(createInterviewVM.CandidateId)).FirstOrDefault();

                string location;
                if (detailScheduleInterview.TypeLocation == 0)//Online
                {
                    location = $"Location : <a href={createInterviewVM.Location}> Join </a><br><br>";
                }
                else
                {
                    location = $"Place : {createInterviewVM.Location} <br><br>";
                }

                //RSACryptoServiceProvider encrypt email custommer
                var rsaHelper = new RsaHelper();
                var ScheduleId = rsaHelper.Encrypt(scheduleInterview.ScheduleInterviewId);

                string mailTo = (createInterviewVM.ScheduleFollowBy == "candidate") ? createInterviewVM.CandidateEmail : createInterviewVM.CustomerEmail;
                string recipientName = (createInterviewVM.ScheduleFollowBy == "candidate") ? scheduleInterview.Candidate.Name : createInterviewVM.CustomerName;
                string userFollow = (createInterviewVM.ScheduleFollowBy == "candidate") ? "Interviewer" : "Candidate";
                string formConfirmSchedule = $"{myConfiguration.BaseUrlClient}interview/confirmation/{createInterviewVM.ScheduleFollowBy}?s={ScheduleId}";
                string subjectMail = "Interview Schedule Confirmation";
                string bodyMail = $"Dear {recipientName}<br><br> " +
                    $"You will conduct an interview with the following details: <br>" +
                    $"Company : <b>{createInterviewVM.CompanyName}</b><br>" +
                    $"Position : <b>{createInterviewVM.JobTitle}</b><br>" +
                    $"Candidate : <b>{candidateData.Name}</b><br>" +
                    $"Customer : <b>{createInterviewVM.CustomerName}</b><br>" +
                    $"{location}" +
                    $"Please choose three schedule you can, We will contact {userFollow} for follow your schedule : <br>" +
                    $"<a href='{formConfirmSchedule}'> Select schedule</a><br><br>" +
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

      

        internal int CreateDateOption(CreateDateOptionsVM createDateOptionsVM)
        {
            //update status schedule to ITV-WC (WAITING CONFIRM)
            var scheduleInterviewData = myContext.ScheduleInterviews.Where(x => x.ScheduleInterviewId == createDateOptionsVM.ScheduleInterviewId).FirstOrDefault();
            scheduleInterviewData.StatusId = "ITV-WC";
            scheduleInterviewData.UpdatedAt = DateTime.Now;
            Update(scheduleInterviewData);
            myContext.SaveChanges();

            //add to Entity scheduleInterviewDateOptions
            ScheduleInterviewDateOption scheduleInterviewDateOptionOne = new ScheduleInterviewDateOption(
                createDateOptionsVM.ScheduleInterviewId,
                (DateTime)createDateOptionsVM.DateTimeOne,
                DateTime.Now,
                DateTime.Now
            );
            myContext.ScheduleInterviewDateOptions.Add(scheduleInterviewDateOptionOne);
            myContext.SaveChanges();

            ScheduleInterviewDateOption scheduleInterviewDateOptionTwo = new ScheduleInterviewDateOption(
                createDateOptionsVM.ScheduleInterviewId,
                (DateTime)createDateOptionsVM.DateTimeTwo,
                DateTime.Now,
                DateTime.Now
            );
            myContext.ScheduleInterviewDateOptions.Add(scheduleInterviewDateOptionTwo);
            myContext.SaveChanges();

            ScheduleInterviewDateOption scheduleInterviewDateOptionThree = new ScheduleInterviewDateOption(
               createDateOptionsVM.ScheduleInterviewId,
               (DateTime)createDateOptionsVM.DateTimeThree,
               DateTime.Now,
               DateTime.Now
            );
            myContext.ScheduleInterviewDateOptions.Add(scheduleInterviewDateOptionThree);
            myContext.SaveChanges();

            //get detail data scheduleInterview
            var detaildataScheduleInterview = myContext.DetailScheduleInterviews.Where(x => x.ScheduleInterviewId.Equals(createDateOptionsVM.ScheduleInterviewId)).FirstOrDefault();

            //send result to email
            string mailTo = (createDateOptionsVM.ScheduleFollowBy == "candidate") ? detaildataScheduleInterview.EmailCandidate : detaildataScheduleInterview.EmailCustomer;
            string recipientName = (createDateOptionsVM.ScheduleFollowBy == "candidate") ? detaildataScheduleInterview.ScheduleInterview.Candidate.Name : detaildataScheduleInterview.ScheduleInterview.CustomerName;
            string withName = (createDateOptionsVM.ScheduleFollowBy != "candidate") ? detaildataScheduleInterview.ScheduleInterview.Candidate.Name : detaildataScheduleInterview.ScheduleInterview.CustomerName;
            string userFollow = (createDateOptionsVM.ScheduleFollowBy == "candidate") ? "Interviewer" : "Candidate";

            string ScheduleDate1 = scheduleInterviewDateOptionOne.DateInterview.ToString("dddd, dd MMMM hh:mm tt");
            string ScheduleDate2 = scheduleInterviewDateOptionTwo.DateInterview.ToString("dddd, dd MMMM hh:mm tt");
            string ScheduleDate3 = scheduleInterviewDateOptionThree.DateInterview.ToString("dddd, dd MMMM hh:mm tt");

            string linkSchedule1 = $"{myConfiguration.BaseUrlClient}interview/confirmation/{createDateOptionsVM.ScheduleInterviewId}/{scheduleInterviewDateOptionOne.Id}";
            string linkSchedule2 = $"{myConfiguration.BaseUrlClient}interview/confirmation/{createDateOptionsVM.ScheduleInterviewId}/{scheduleInterviewDateOptionTwo.Id}";
            string linkSchedule3 = $"{myConfiguration.BaseUrlClient}interview/confirmation/{createDateOptionsVM.ScheduleInterviewId}/{scheduleInterviewDateOptionThree.Id}";


            string subjectMail = "Interview Schedule Confirmation";
            string bodyMail =

                $"The date you chose to schedule the interview with {withName}: <br>" +
                $"1. <b>{ScheduleDate1}</b><br>" +
                $"2. <b>{ScheduleDate2}</b><br>" +
                $"3. <b>{ScheduleDate3}</b><br>" +
                $"Please wait for a response from the {userFollow} for interview scheduling. <br>" +
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

            //checktype
                string location;
                if (detaildataScheduleInterview.TypeLocation == 0)//Online
                {
                    location = $"Location : <a href={detaildataScheduleInterview.ScheduleInterview.Location}> Join </a><br><br>";
                }
                else
                {
                    location = $"Location : {detaildataScheduleInterview.ScheduleInterview.Location} <br><br>";
                }

            //send response Confirmation Date
            string userFollow2 = (createDateOptionsVM.ScheduleFollowBy != "candidate") ? "interviewer" : "candidate";
            string mailTo2 = (createDateOptionsVM.ScheduleFollowBy == "candidate") ? detaildataScheduleInterview.EmailCustomer : detaildataScheduleInterview.EmailCandidate;
            string subjectMail2 = "Interview Schedule Confirmation";
            string bodyMail2 =

                $"Dear {withName}<br><br> " +
                $"You will conduct an interview with the following details: <br>" +
                $"Company : <b>{detaildataScheduleInterview.ScheduleInterview.Company.Name}</b><br>" +
                $"Position : <b>{detaildataScheduleInterview.ScheduleInterview.JobTitle}</b><br>" +
                $"Candidate : <b>{detaildataScheduleInterview.ScheduleInterview.Candidate.Name}</b><br>" +
                $"Customer : <b>{detaildataScheduleInterview.ScheduleInterview.CustomerName}</b><br>" +
                $"{location}" +

                $"Please choose a schedule you can, We will contact {userFollow2} : <br>" +
                $"<a href={linkSchedule1}> {ScheduleDate1}</a><br><br>" +
                $"<a href={linkSchedule2}> {ScheduleDate2}</a><br><br>" +
                $"<a href={linkSchedule3}> {ScheduleDate3}</a><br><br>" +
                $"<br><br>" +
                $"[Distribution System]";


            MailHelper mailHelper2 = new MailHelper();
            mailHelper2.SmtpClient(
                subjectMail2,
                bodyMail2,
                mailTo2,
                myConfiguration.From,
                myConfiguration.Email,
                myConfiguration.Password,
                myConfiguration.SmtpServer,
                myConfiguration.Port
            );

            return 1;
        }

        internal string ResponseConfirmationDate(InterviewResponseVM interviewResponseVM)
        {
            try
            {
                //get data dateschedulefix
                var dateSceduleFixData = myContext.ScheduleInterviewDateOptions.Where(x => x.Id == interviewResponseVM.ScheduleDateConfirmId).FirstOrDefault();


                //update status schedule to ITV-OG (interview on going) and datetime interview
                var scheduleInterviewData = myContext.ScheduleInterviews.Where(x => x.ScheduleInterviewId == interviewResponseVM.ScheduleInterviewId).FirstOrDefault();
                scheduleInterviewData.StatusId = "ITV-OG";
                scheduleInterviewData.StartInterview = dateSceduleFixData.DateInterview;
                scheduleInterviewData.UpdatedAt = DateTime.Now;
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

                //RSACryptoServiceProvider encrypt email custommer
                var rsaHelper = new RsaHelper();
                var Emailencrypted = rsaHelper.Encrypt(detailScheduleInterviewData.EmailCustomer);

                string linkAccept = $"{myConfiguration.BaseUrlClient}/interview/{scheduleInterviewData.ScheduleInterviewId}/ACCEPTED?e={Emailencrypted}";
                string linkCancel = $"{myConfiguration.BaseUrlClient}/interview/{scheduleInterviewData.ScheduleInterviewId}/CANCELED?e={Emailencrypted}";

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

        internal string ConfirmationAcceptedCandidate(InterviewResponseVM interviewResponseVM)
        {
            try
            {
                //get data scheduleinterview data
                var scheduleInterviewData = myContext.ScheduleInterviews.Where(x => x.ScheduleInterviewId == interviewResponseVM.ScheduleInterviewId).FirstOrDefault();

                //check data scheduleinterview
                if (scheduleInterviewData == null)
                {
                    return "404";
                }

                var detailScheduleInterviewData = myContext.DetailScheduleInterviews.Where(x => x.ScheduleInterviewId == scheduleInterviewData.ScheduleInterviewId).FirstOrDefault();
                
                //credential email customer
                if (detailScheduleInterviewData.EmailCustomer != interviewResponseVM.EmailCustomer) {

                    return "401";
                }

                //if candidate accepted
                if (interviewResponseVM.CandidateAccepted == "ACCEPTED")
                {
                    //update status schedule to ITV-DN(interview done) candidate accepted 
                    scheduleInterviewData.StatusId = "ITV-DN";
                    scheduleInterviewData.UpdatedAt = DateTime.Now;
                    Update(scheduleInterviewData);
                    myContext.SaveChanges();

                    //update status candidate
                    var candidateData = myContext.Candidates.Where(x => x.CandidateId == scheduleInterviewData.CandidateId).FirstOrDefault();
                    candidateData.Status = status.Waiting;
                    candidateData.UpdatedAt = DateTime.Now;
                    myContext.Candidates.Update(candidateData);
                    myContext.SaveChanges();

                    //send feedback
                    string subjectMail2 = "[INTERVIEW] Congratulations, you passed the job interview stage!";
                    string bodyMail2 =
                        $"Dear {scheduleInterviewData.Candidate.Name}<br><br> " +
                        $"Thank you for your participation in the job interview process at {scheduleInterviewData.Company.Name}. <br>" +
                        $"Congratulations, you got the job! <br>" +
                        $"for the next, wait for information from us for scheduling your work.</b><br><br>" +
                        $"[Distribution System]";


                    MailHelper mailToCustromer = new MailHelper();
                    mailToCustromer.SmtpClient(
                        subjectMail2,
                        bodyMail2,
                        detailScheduleInterviewData.EmailCandidate,
                        myConfiguration.From,
                        myConfiguration.Email,
                        myConfiguration.Password,
                        myConfiguration.SmtpServer,
                        myConfiguration.Port
                    );

                    return "Success! candidate accepted";
                }

                //if candidate canceled
                if (interviewResponseVM.CandidateAccepted == "CANCELED")
                {
                    //update status schedule to ITV-CN(interview canceled) candidate canceled
                    scheduleInterviewData.StatusId = "ITV-CN";
                    scheduleInterviewData.UpdatedAt = DateTime.Now;
                    Update(scheduleInterviewData);
                    myContext.SaveChanges();

                    //update status candidate
                    var candidateData = myContext.Candidates.Where(x => x.CandidateId == scheduleInterviewData.CandidateId).FirstOrDefault();
                    candidateData.Status = status.Idle;
                    candidateData.UpdatedAt = DateTime.Now;
                    myContext.Candidates.Update(candidateData);
                    myContext.SaveChanges();

                    //send feedback
                    string subjectMail2 = "[INTERVIEW] Sorry, you haven't made it yet";
                    string bodyMail2 =
                        $"Dear {scheduleInterviewData.Candidate.Name}<br><br> " +
                        $"Thank you for your participation in the job interview process at {scheduleInterviewData.Company.Name}. <br>" +
                        $"We would like to inform you are not yet qualified to pass the interview . <br>" +
                        $"wait for more information from us.</b><br><br>" +
                        $"[Distribution System]";


                    MailHelper mailToCustromer = new MailHelper();
                    mailToCustromer.SmtpClient(
                        subjectMail2,
                        bodyMail2,
                        detailScheduleInterviewData.EmailCandidate,
                        myConfiguration.From,
                        myConfiguration.Email,
                        myConfiguration.Password,
                        myConfiguration.SmtpServer,
                        myConfiguration.Port
                    );

                    return "Success! candidate canceled";
                }

                return "404";

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