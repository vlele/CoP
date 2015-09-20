using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailActorService
{
    public class SendGridService
    {
        private static string from;
        private static string sub;
        private static string bodyHtml;
        private static string username;
        private static string password;

        static SendGridService()
        {
            from = "";
            sub = "Cloud Sample Mail!";
            bodyHtml = "<p>{0}</p>";
            username = "";
            password = "";
        }

        public static bool SendEmail(string to, string body)
        {
            SendGridMessage email = new SendGridMessage()
            {
                From = new MailAddress(from),
                Subject = sub,
                Html = String.Format(bodyHtml, body)
            };

            bool retVal = true;

            try
            {
                email.AddTo(to.Split(new char[] { ',' }));
                SendGrid.Web webTransport = new SendGrid.Web(new NetworkCredential(username, password));
                webTransport.DeliverAsync(email);
            }
            catch (Exception ex)
            {
                retVal = false;
            }

            return retVal;
        }
    }
}
