//--------------------------------------------------------------------------------------------------
// This code is the property of GainWell Technology, Copyright (c) 2022. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using HPP.OneClick.XAML.Migration.DevContext;
using SendGrid;
using SendGrid.Helpers.Mail;


namespace HPP.OneClick.XAML.Migration
{
    public class LongRunning
    {
        DBAccess db;
        public LongRunning()
        {
            db = new DBAccess();
            Console.WriteLine("Long running constructor");
        }

        public void ProcessandTrigger()
        {
            List<BUILD_QUEUE> bq = get_AverageTime();
            if (bq.Count > 0)
            {
                Execute(bq).Wait();
            }
        }

        public List<BUILD_QUEUE> get_AverageTime()
        {
            List<BUILD_QUEUE> actionNeededSln = new List<BUILD_QUEUE>();
            Dictionary<string, int> dicdata = new Dictionary<string, int>();
            dicdata.Add("InProgress", 30);
            dicdata.Add("Queue", 120);
            dicdata.Add("Wait", 120);
            dicdata.Add("PackageCheckIn", 120);


            db.GetLongrunningSln(dicdata).ForEach(a =>
            {
                double highestavgtime = db.Get_AverageTime(a.SOLUTION_NAME_FULL_PATH);
                TimeSpan? ts;
                if (a.BUILD_STATUS.ToLower() == "inprogress")
                {
                    ts = DateTime.UtcNow - a.BUILD_START_TS;
                }
                else
                {
                    ts = DateTime.UtcNow - a.LAST_MODIFIED_TS;
                }
                if (ts.Value.TotalMinutes > highestavgtime)
                {
                    actionNeededSln.Add(a);
                }
            });

            return actionNeededSln;
        }

        public async Task Execute(List<BUILD_QUEUE> buildqueue)
        {
            //var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var apiKey = System.Configuration.ConfigurationManager.AppSettings["ApiKey"];
            var fromemail = System.Configuration.ConfigurationManager.AppSettings["FromEmail"];
            var toemail = System.Configuration.ConfigurationManager.AppSettings["ToEmail"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromemail, "Srinivasan");
            var subject = "Long Running Builds in jenkins";
            var to = new EmailAddress(toemail, "Example User");
            List<EmailAddress> tolist = new List<EmailAddress>()
            {
                new EmailAddress("msrinivasan@gainwelltechnologies.com"),
                new EmailAddress("pradheep.sengottaiyan@gainwelltechnologies.com"),
               // new EmailAddress("raghavan.ramanujam@gainwelltechnologies.com"),
               // new EmailAddress("hariharan.sundara-raju@gainwelltechnologies.com"),
            };
            var plainTextContent = "and easy to do anywhere, even with C#";
            //var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            string htmlContent = ComposeEmailBody(buildqueue);

            var msg1 = MailHelper.CreateSingleEmailToMultipleRecipients(from, tolist, subject, plainTextContent, htmlContent);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            //var response = await client.SendEmailAsync(msg);
            var response = await client.SendEmailAsync(msg1);
        }

        private string ComposeEmailBody(List<BUILD_QUEUE> buildqueue)
        {
            StringBuilder htmlContent = new StringBuilder();
            htmlContent.Append("<strong>Following  " + buildqueue.Count  + " Builds/Solutions are running for long time </strong>");
            buildqueue.ForEach(a =>
            {
                TimeSpan? ts;
                //if (a.BUILD_STATUS.ToLower() == "inprogress")
                //{
                //    ts = DateTime.UtcNow - a.BUILD_START_TS;

                    
                //    double buildMinutes = (ts.Value.Days * 1440) + (ts.Value.Hours * 60) + (ts.Value.Minutes) + (ts.Value.Seconds > 0 ? 1 : 0);
                //}
                //else
                //{
                    ts = DateTime.UtcNow - a.LAST_MODIFIED_TS;
                    double buildMinutes = (ts.Value.Days * 1440) + (ts.Value.Hours * 60) + (ts.Value.Minutes) + (ts.Value.Seconds > 0 ? 1 : 0);
                //}

                TimeSpan span = TimeSpan.FromMinutes(buildMinutes);

                int hrs = ts.Value.Days > 1 ? ts.Value.Days * 24:0;
                int min = ts.Value.Minutes;

                string elapsetime = string.Concat(hrs, ":", min);

                htmlContent.Append("<br/>Status : " + a.BUILD_STATUS + 
                    ", SolutionName : "+ a.SOLUTION_NAME_FULL_PATH + ", This builds are running from last " + elapsetime + " hours");

            });

            htmlContent.Append("<br/><strong>this is system generated email no need to replay </strong>");
            return htmlContent.ToString();
        }
    }
}