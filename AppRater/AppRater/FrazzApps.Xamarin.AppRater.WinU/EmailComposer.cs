using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.AppRater.WinU;
using FrazzApps.Xamarin.AppRater.Abstractions;
using Windows.ApplicationModel.Email;

[assembly: Dependency(typeof(EmailComposer))]
namespace FrazzApps.Xamarin.AppRater.WinU
{
    /// <summary>
    /// Rater Implementation
    /// </summary>
    internal class EmailComposer : IEmailComposer
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        internal static void Init() { }
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
