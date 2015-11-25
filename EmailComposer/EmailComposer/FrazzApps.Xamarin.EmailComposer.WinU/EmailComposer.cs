using FrazzApps.Xamarin.EmailComposer.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.EmailComposer.WinU;
using Windows.ApplicationModel.Email;

[assembly: Dependency(typeof(EmailComposer))]
namespace FrazzApps.Xamarin.EmailComposer.WinU
{
    /// <summary>
    /// EmailComposer Implementation
    /// </summary>
    public class EmailComposer : IEmailComposer
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { }
        public EmailComposer() { }

        public void SendEmail(string toAddress, string subject, string body)
        {

            var emailMessage = new EmailMessage();
            emailMessage.Body = body;
            
            emailMessage.Subject = subject;
            emailMessage.Body = body;

            var emailRecipient = new EmailRecipient(toAddress);
            emailMessage.To.Add(emailRecipient);

            EmailManager.ShowComposeNewEmailAsync(emailMessage);
        }
    }
}
