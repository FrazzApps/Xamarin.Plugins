using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FrazzApps.Xamarin.AzureNotifier.Android
{
    internal class Constants
    {
        public const string SenderID = "<GoogleProjectNumber>"; // Google API Project Number

        // Azure app specific connection string and hub path
        public const string ConnectionString = "Endpoint=sb://frazzappshub-ns.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=0nHzeCx7g8ljXP4a22BXWvI6UtSOCoLDgKTxRS9AYmw=";
        public const string NotificationHubPath = "frazzappshub";

          
    }
}