using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrazzApps.Xamarin.AzureNotifier
{
    public class AzureNotificationEventArgs
    {
        public AzureNotificationEventArgs(String message)
        { this.Message = message; }
        public AzureNotificationEventArgs(String message, string registrationId)
        { this.Message = message; this.RegistrationId = registrationId; }

        public String Message { get; private set; }

        public String RegistrationId { get; private set; }
    }
}
