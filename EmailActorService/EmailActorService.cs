using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmailActorService.Interfaces;
using Microsoft.ServiceFabric;
using Microsoft.ServiceFabric.Actors;
using EmailActorService.Interfaces.Models;

namespace EmailActorService
{
    public class EmailActorService : Actor, IEmailActorService
    {
        public Task<bool> SendEmail(string to, string message, string thandle)
        {
            bool emailSent = SendGridService.SendEmail(to, message);
            EmailDataService.Insert(to, message, thandle, (emailSent ? "SENT" : "ERROR"));
            return Task.FromResult(emailSent);
        }

        public Task<IEnumerable<EmailModel>> EmailHistory(string fromDate, string toDate)
        {
            IList<EmailModel> emailList = new List<EmailModel>();
            var emails = EmailDataService.GetEmails(fromDate, toDate);

            foreach (var email in emails)
            {
                emailList.Add(new EmailModel()
                {
                    RequestDateTime = email.Timestamp,
                    Status = email.Status,
                    To = email.To,
                    Message = email.Message
                });
            }

            return Task.FromResult<IEnumerable<EmailModel>>(emailList);
        }
    }
}
