using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SendGridSample
{
    class Program
    {
        static void Main(string[] args)
        {
            TestMail();
            Console.Read();
        }

        private static void TestMail()
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                using (SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["SmtpHost"], 25))
                {
                    client.EnableSsl = false;
                    client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SmtpUserName"], ConfigurationManager.AppSettings["SmtpPassword"]);

                    //MailAddress from = new MailAddress("vtproviderenrollment@dxc.com");
                    Console.Write("Enter Email Id: ");
                    string emailAddress = Console.ReadLine();
                    MailAddress from = new MailAddress(emailAddress);
                    MailAddress to = new MailAddress(emailAddress);

                    MailMessage message = new MailMessage(from, to)
                    {
                        Subject = "TESTING - Account verification code email",
                        Body = File.ReadAllText("sample_email.txt", Encoding.ASCII),
                        Priority = MailPriority.High,
                        IsBodyHtml = true
                    };
                    Console.WriteLine(DateTime.Now + ": Trying to send Email");
                    sw.Start();
                    client.Send(message);
                    sw.Stop();
                    Console.WriteLine(DateTime.Now + ": Email sent!!. Time taken (ms): " + sw.ElapsedMilliseconds);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            
        }
    }
}
