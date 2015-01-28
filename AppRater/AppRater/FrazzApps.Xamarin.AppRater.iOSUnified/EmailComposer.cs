using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.AppRater.iOSUnified;
using FrazzApps.Xamarin.AppRater.Abstractions;
using MessageUI;
using UIKit;

[assembly: Dependency(typeof(FrazzApps.Xamarin.AppRater.iOSUnified.EmailComposer))]
namespace FrazzApps.Xamarin.AppRater.iOSUnified
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
            MFMailComposeViewController _mailController = new MFMailComposeViewController();
            _mailController.SetToRecipients(new string[] { toAddress });
            _mailController.SetSubject(subject);
            _mailController.SetMessageBody(body, false);

            _mailController.Finished += (object s, MFComposeResultEventArgs args) =>
            {
                Console.WriteLine(args.Result.ToString());
                args.Controller.DismissViewController(true, null);
            };

            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(_mailController, true, null);            
        }


    }
}
