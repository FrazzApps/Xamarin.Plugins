using System;

namespace FrazzApps.Xamarin.EmailComposer.Abstractions
{
    /// <summary>
    /// EmailComposer Interface
    /// </summary>
    public interface IEmailComposer
    {
        void SendEmail(string toAddress, string subject, string body);
    }
}
