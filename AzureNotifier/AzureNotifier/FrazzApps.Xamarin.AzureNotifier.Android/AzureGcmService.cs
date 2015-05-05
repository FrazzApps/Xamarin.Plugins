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

using Gcm.Client;
using WindowsAzure.Messaging;
using Android.Util;
using Android.Support.V4.App;

namespace FrazzApps.Xamarin.AzureNotifier.Android
{
    [Service] //Must use the service tag
    public class AzureGcmService : GcmServiceBase
    {
        static NotificationHub hub;

        public static void Initialize(Context context, string connectionString, string notificationHubName, string key)
        {
            // Call this from our main activity
            var cs = ConnectionString.CreateUsingSharedAccessKeyWithListenAccess(
                new Java.Net.URI(connectionString),
                key);

            var hubName = notificationHubName;

            hub = new NotificationHub(hubName, cs, context);
        }

        public static void Register(Context Context)
        {
            // Makes this easier to call from our Activity
      //      GcmClient.Register(Context, AzureBroadcastReceiver.SENDER_IDS);
        }

        public AzureGcmService()
       //     : base(AzureBroadcastReceiver.SENDER_IDS)
        {
        }

        protected override void OnRegistered(Context context, string registrationId)
        {
            //Receive registration Id for sending GCM Push Notifications to

            if (hub != null)
                hub.Register(registrationId, "TEST");
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            if (hub != null)
                hub.Unregister();
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            Console.WriteLine("Received Notification");

            //Push Notification arrived - print out the keys/values
            if (intent != null || intent.Extras != null)
            {

                var keyset = intent.Extras.KeySet();

                foreach (var key in intent.Extras.KeySet())
                    Console.WriteLine("Key: {0}, Value: {1}", key, intent.Extras.GetString(key));
            }
        }

        protected override bool OnRecoverableError(Context context, string errorId)
        {
            //Some recoverable error happened
            return true;
        }

        protected override void OnError(Context context, string errorId)
        {
            //Some more serious error happened
        }
    }
}