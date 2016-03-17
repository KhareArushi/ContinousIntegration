using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContinousIntegration.Models
{
    public class MessageServices
    {
        public  static void SendEmail(string email, string subject, string message)
        {
            var _email = ConfigurationManager.AppSettings["FromMail"];
            var _epass = ConfigurationManager.AppSettings["EmailPassword"];
            var _dispName = ConfigurationManager.AppSettings["CI_Team"];
            MailMessage mymessage = new MailMessage();
            mymessage.To.Add(new MailAddress(email));
            mymessage.From = new MailAddress(_email, _dispName);
            mymessage.Subject = subject;
            mymessage.Body = message;
            mymessage.IsBodyHtml = Convert.ToBoolean(ConfigurationManager.AppSettings["IsBody_Html"]);

            using (SmtpClient smtp = new SmtpClient())
            {
                try
                {
                    smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                    smtp.Host = ConfigurationManager.AppSettings["Host"];
                    smtp.Port = Convert.ToInt16(ConfigurationManager.AppSettings["Port"]);
                    smtp.UseDefaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["UseDefault_Credentials"]);
                    smtp.Credentials = new NetworkCredential(_email, _epass);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(mymessage);
                }
                catch (Exception ex)
                {
                   throw ex;
                }
            }
        }
    }
}
