using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrazzApps.Xamarin.AzureNotifier
{
    public class AzureNotificationErrorEventArgs
    {
        public AzureNotificationErrorEventArgs(String message)
        { this.Message = message; }

        public String Message { get; private set; }
    }
}
