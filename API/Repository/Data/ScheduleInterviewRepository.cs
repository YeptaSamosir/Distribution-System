using System;
using System.Collections.Generic;
using System.IO;
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

        internal string CreateInterview(CreateInterviewVM createInterviewVM)
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

                //validation data
                var resultData = myContext.ScheduleInterviews.Find(ScheduleInterviewId);
                if (resultData != null) {
                    return "Data already exists";
                }

                //add to Entity ScheduleInterview
                ScheduleInterview scheduleInterview = new ScheduleInterview(
                    ScheduleInterviewId,
                    DateTime.MinValue,
                    createInterviewVM.CandidateId,
                    companyId,
                    createInterviewVM.CustomerName,
                    createInterviewVM.JobTitle,
                    createInterviewVM.Location,
                    createInterviewVM.ScheduleFollowBy,
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

                string typeLocation = (detailScheduleInterview.TypeLocation == 0) ? "Online" : "Offline" ;

                //RSACryptoServiceProvider encrypt email custommer
                var rsaHelper = new RsaHelper();
                var ScheduleId = rsaHelper.Encrypt(scheduleInterview.ScheduleInterviewId);

                string mailTo = (createInterviewVM.ScheduleFollowBy == "candidate") ? createInterviewVM.CandidateEmail : createInterviewVM.CustomerEmail;
                string recipientName = (createInterviewVM.ScheduleFollowBy == "candidate") ? scheduleInterview.Candidate.Name : createInterviewVM.CustomerName;
                string userFollow = (createInterviewVM.ScheduleFollowBy == "candidate") ? "Interviewer" : "Candidate";
                string formConfirmSchedule = $"{myConfiguration.BaseUrlClient}interview/confirmation/{createInterviewVM.ScheduleFollowBy}?s={ScheduleId}";
                string subjectMail = "[INTERVIEW SCHEDULE CONFIRMATION] Confirm date selection for interview";

                //Fetching Email Body Text from EmailTemplate File.  
                string FilePath = @"..\Client\\wwwroot\\assets\\email_template\\confirmdate.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();

                string title = "Confirm date selection for interview";
                //Repalce dinamic text  
                MailText = MailText.Replace("[title]", title)
                    .Replace("[recipientName]", recipientName)
                    .Replace("[companyName]", createInterviewVM.CompanyName)
                    .Replace("[jobTitle]", createInterviewVM.JobTitle)
                    .Replace("[candidateName]", candidateData.Name)
                    .Replace("[customerName]", createInterviewVM.CustomerName)
                    .Replace("[typeLocation]", typeLocation)
                    .Replace("[linkLocation]", createInterviewVM.Location)
                    .Replace("[userFollow]", userFollow)
                    .Replace("[linkDates]", formConfirmSchedule);

                MailHelper mailHelper = new MailHelper();
                mailHelper.SmtpClient(
                    subjectMail,
                    MailText,
                    mailTo,
                    myConfiguration.From,
                    myConfiguration.Email,
                    myConfiguration.Password,
                    myConfiguration.SmtpServer,
                    myConfiguration.Port
                );
                return "Success Create Schedule";
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


            string subjectMail = "[INTERVIEW SCHEDULE CONFIRMATION] Confirm date selection for interview";
        

            //Fetching Email Body Text from EmailTemplate File.  
            string FilePath = @"..\Client\\wwwroot\\assets\\email_template\\confirmdateresult.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();

            string title = "Confirm date selection for interview result";
            //Repalce dinamic text  
            MailText = MailText.Replace("[title]", title)
                .Replace("[withName]", withName)
                .Replace("[ScheduleDate1]", ScheduleDate1)
                .Replace("[ScheduleDate2]", ScheduleDate2)
                .Replace("[ScheduleDate3]", ScheduleDate3)
                .Replace("[userFollow]", userFollow);

            MailHelper mailHelper = new MailHelper();
            mailHelper.SmtpClient(
                subjectMail,
                MailText,
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

            string typeLocation = (detaildataScheduleInterview.TypeLocation == 0) ? "Online" : "Offline";

            //send response Confirmation Date
            string userFollow2 = (createDateOptionsVM.ScheduleFollowBy != "candidate") ? "interviewer" : "candidate";
            string mailTo2 = (createDateOptionsVM.ScheduleFollowBy == "candidate") ? detaildataScheduleInterview.EmailCustomer : detaildataScheduleInterview.EmailCandidate;
            string subjectMail2 = "[INTERVIEW SCHEDULE CONFIRMATION] Confirm date selection for interview";

            //Fetching Email Body Text from EmailTemplate File.  
            string FilePath2 = @"..\Client\\wwwroot\\assets\\email_template\\confirmdatecandidate.html";
            StreamReader str2 = new StreamReader(FilePath2);
            string MailText2 = str2.ReadToEnd();
            str2.Close();

            string title2 = "Confirm date selection for interview";
         
            //Repalce dinamic text  
            MailText2 = MailText2.Replace("[title]", title2)
                .Replace("[recipientName]", withName)
                .Replace("[companyName]", detaildataScheduleInterview.ScheduleInterview.Company.Name)
                .Replace("[jobTitle]", detaildataScheduleInterview.ScheduleInterview.JobTitle)
                .Replace("[candidateName]", detaildataScheduleInterview.ScheduleInterview.Candidate.Name)
                .Replace("[customerName]", detaildataScheduleInterview.ScheduleInterview.CustomerName)
                .Replace("[typeLocation]", typeLocation)
                .Replace("[linkLocation]", detaildataScheduleInterview.ScheduleInterview.Location)
                .Replace("[userFollow]", userFollow2)
                .Replace("[linkSchedule1]", linkSchedule1)
                .Replace("[linkSchedule2]", linkSchedule2)
                .Replace("[linkSchedule3]", linkSchedule3)
                .Replace("[ScheduleDate1]", ScheduleDate1)
                .Replace("[ScheduleDate2]", ScheduleDate2)
                .Replace("[ScheduleDate3]", ScheduleDate3)
                ;

            MailHelper mailHelper2 = new MailHelper();
            mailHelper2.SmtpClient(
                subjectMail2,
                MailText2,
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


                string typeLocation = (detailScheduleInterviewData.TypeLocation == 0) ? "Online" : "Offline";
                //send email schedule to candidate
                string subjectMail1 = "[INTERVIEW] Invitations to interview";
                
                //Fetching Email Body Text from EmailTemplate File.  
                string FilePath = @"..\Client\\wwwroot\\assets\\email_template\\interviewcandidate.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();

                string title = "Invitations to interview";
                //Repalce dinamic text  
                MailText = MailText.Replace("[title]", title)
                    .Replace("[recipientName]", scheduleInterviewData.Candidate.Name)
                    .Replace("[companyName]", scheduleInterviewData.Company.Name)
                    .Replace("[jobTitle]", scheduleInterviewData.JobTitle)
                    .Replace("[candidateName]", scheduleInterviewData.CustomerName)
                    .Replace("[customerName]", scheduleInterviewData.CustomerName)
                    .Replace("[typeLocation]", typeLocation)
                    .Replace("[linkLocation]", scheduleInterviewData.Location)
                    .Replace("[Date]", scheduleInterviewData.StartInterview.ToString("dddd, dd MMMM hh:mm tt"));

                MailHelper mailToCandidate = new MailHelper();
                mailToCandidate.SmtpClient(
                    subjectMail1,
                    MailText,
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

                string subjectMail2 = "[INTERVIEW] Invitations to interview";

                //Fetching Email Body Text from EmailTemplate File.  
                string FilePath2 = @"..\Client\\wwwroot\\assets\\email_template\\interviewcustomer.html";
                StreamReader str2 = new StreamReader(FilePath2);
                string MailText2 = str2.ReadToEnd();
                str2.Close();

                string title2 = "Invitations to interview";
                //Repalce dinamic text  
                MailText2 = MailText2.Replace("[title]", title2)
                    .Replace("[recipientName]", scheduleInterviewData.CustomerName)
                    .Replace("[companyName]", scheduleInterviewData.Company.Name)
                    .Replace("[jobTitle]", scheduleInterviewData.JobTitle)
                    .Replace("[candidateName]", scheduleInterviewData.CustomerName)
                    .Replace("[customerName]", scheduleInterviewData.CustomerName)
                    .Replace("[typeLocation]", typeLocation)
                    .Replace("[linkLocation]", scheduleInterviewData.Location)
                    .Replace("[Date]", scheduleInterviewData.StartInterview.ToString("dddd, dd MMMM hh:mm tt"))
                    .Replace("[linkAccept]", linkAccept)
                    .Replace("[linkCancel]", linkCancel)
                    ;
                    
                    
                MailHelper mailToCustromer = new MailHelper();
                mailToCustromer.SmtpClient(
                    subjectMail2,
                    MailText2,
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
                    //update status schedule to ITV-AC (candidate accepted)
                    scheduleInterviewData.StatusId = "ITV-AC";
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
                    string subjectMail2 = "[INTERVIEW] Update the interview results";

                    //Fetching Email Body Text from EmailTemplate File.  
                    string FilePath = @"..\Client\wwwroot\assets\email_template\accepted.html";
                    StreamReader str = new StreamReader(FilePath);
                    string MailText = str.ReadToEnd();
                    str.Close();

                    string title = "Update the interview results";
                    //Repalce dinamic text  
                    MailText = MailText.Replace("[title]", title)
                        .Replace("[recipientName]", scheduleInterviewData.Candidate.Name)
                        .Replace("[companyName]", scheduleInterviewData.Company.Name);

                    MailHelper mailToCustromer = new MailHelper();
                    mailToCustromer.SmtpClient(
                        subjectMail2,
                        MailText,
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
                    string subjectMail2 = "[INTERVIEW] Update the interview results";

                    //Fetching Email Body Text from EmailTemplate File.  
                    string FilePath2 = @"..\Client\wwwroot\assets\email_template\canceled.html";
                    StreamReader str2 = new StreamReader(FilePath2);
                    string MailText2 = str2.ReadToEnd();
                    str2.Close();

                    string title = "Update the interview results";
                    //Repalce dinamic text  
                    MailText2 = MailText2.Replace("[title]", title)
                        .Replace("[recipientName]", scheduleInterviewData.Candidate.Name)
                        .Replace("[companyName]", scheduleInterviewData.Company.Name);


                    MailHelper mailToCustromer = new MailHelper();
                    mailToCustromer.SmtpClient(
                        subjectMail2,
                        MailText2,
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