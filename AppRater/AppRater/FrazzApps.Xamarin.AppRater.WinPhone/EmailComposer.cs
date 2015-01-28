using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.AppRater.WinPhone;
using FrazzApps.Xamarin.AppRater.Abstractions;
using Microsoft.Phone.Tasks;

[assembly: Dependency(typeof(FrazzApps.Xamarin.AppRater.WinPhone.EmailComposer))]
namespace FrazzApps.Xamarin.AppRater.WinPhone
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
            EmailComposeTask emailComposeTask = new EmailComposeTask();

            emailComposeTask.Subject = subject;
            emailComposeTask.Body = body;
            emailComposeTask.To = toAddress;

            emailComposeTask.Show();
        }


    }
}
