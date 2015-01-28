using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.EmailComposer.Android;
using FrazzApps.Xamarin.EmailComposer.Abstractions;
using Android.Content;
using Android.App;

[assembly: Dependency(typeof(EmailComposer))]
namespace FrazzApps.Xamarin.EmailComposer.Android
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
            var email = new Intent(Intent.ActionSend);

            email.PutExtra(Intent.ExtraEmail, new string[] { toAddress });

            //email.PutExtra(Intent.ExtraCc, new string[] { toAddress });
            //email.PutExtra(Intent.ExtraBcc, new string[] { toAddress });

            email.PutExtra(Intent.ExtraSubject, subject);

            email.PutExtra(Intent.ExtraText, body);

            email.SetType("message/rfc822");

            Forms.Context.StartActivity(email);
        }
    }
}
