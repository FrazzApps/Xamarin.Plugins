using FrazzApps.Xamarin.EmailComposer.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.EmailComposer.iOS;
using MonoTouch.MessageUI;
using MonoTouch.UIKit;

[assembly: Dependency(typeof(EmailComposer))]
namespace FrazzApps.Xamarin.EmailComposer.iOS
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
