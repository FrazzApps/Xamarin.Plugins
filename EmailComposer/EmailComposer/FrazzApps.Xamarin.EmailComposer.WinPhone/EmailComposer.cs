using FrazzApps.Xamarin.EmailComposer.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.EmailComposer.WinPhone;
using Microsoft.Phone.Tasks;

[assembly: Dependency(typeof(EmailComposer))]
namespace FrazzApps.Xamarin.EmailComposer.WinPhone
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
            EmailComposeTask emailComposeTask = new EmailComposeTask();

            emailComposeTask.Subject = subject;
            emailComposeTask.Body = body;
            emailComposeTask.To = toAddress;

            emailComposeTask.Show();
        }
    }
}
