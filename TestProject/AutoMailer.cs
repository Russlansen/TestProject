using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using TestProject.Models;

namespace TestProject
{
    public class AutoMailer
    {
        public static void Call(object state)
        {
            if (DateTime.Now.Hour == 14 && DateTime.Now.Minute == 0)
            {
                var db = new ApplicationContext();
                var lastDay = DateTime.Now.Subtract(TimeSpan.FromHours(24));
                string sqlDateString = String.Format("{0}-{1}-{2}T{3}", lastDay.Year, lastDay.Month,
                                                                        lastDay.Day, lastDay.ToLongTimeString());
                var logs = db.Log.Where(x => x.Time.ToString().CompareTo(sqlDateString) >= 0);
                if (logs.Count() == 0)
                    return;
                var subscribers = db.Subscribers;
                var from = new MailAddress("tt9713283@gmail.com", "Рассылка");
                MailMessage message = null;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("tt9713283@gmail.com", "AA123456789");
                smtp.EnableSsl = true;
                foreach (var subscriber in subscribers)
                {
                    var to = new MailAddress(subscriber.E_mail);
                    message = new MailMessage(from, to);
                    message.Subject = "Рассылка";
                    message.IsBodyHtml = true;
                    var messageBody = new StringBuilder();
                    messageBody.Append("<h2>Рассылка</h2><br/>");
                    
                    foreach(var log in logs)
                    {
                        var category = log.Category;
                        if (subscriber.Autos && category.Equals("Autos"))
                            messageBody.Append(log.Message + " в категории Autos<br/>");
                        if (subscriber.Immovables && category.Equals("Immovables"))
                            messageBody.Append(log.Message + " в категории Immovables<br/>");
                        if (subscriber.Pets && category.Equals("Pets"))
                            messageBody.Append(log.Message + " в категории Pets<br/>");
                        if (subscriber.Pictures && category.Equals("Pictures"))
                            messageBody.Append(log.Message + " в категории Pictures<br/>");
                    }
                    message.Body = messageBody.ToString();
                    if (message != null)
                        smtp.Send(message);
                }   
            }
        }
    }
}