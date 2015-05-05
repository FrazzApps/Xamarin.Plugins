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
using System.Threading;

// This will prevent other apps on the device from receiving GCM messages for this app
// It is crucial that the package name does not start with an uppercase letter - this is forbidden by Android.
using Microsoft.WindowsAzure.MobileServices;


[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]

// Gives the app permission to register and receive messages.
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]

// This permission is necessary only for Android 4.0.3 and below.
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]

// Need to access the internet for GCM
[assembly: UsesPermission(Name = "android.permission.INTERNET")]

// Needed to keep the processor from sleeping when a message arrives
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]


namespace FrazzApps.Xamarin.AzureNotifier.Droid
{


    [BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE },
        Categories = new[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK },
        Categories = new[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY },
        Categories = new[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new[] { Intent.ActionBootCompleted })]
    // This attribute is to receive the notification even if the app is not running
    public class MyBroadcastReceiver : GcmBroadcastReceiverBase<PushHandlerService>
    {
        public const string LOG_CATEGORY = "NotificationHubSample-LOG";
        public static string[] SENDER_IDS = { AzureNotifier.SenderID };
    }


    [Service] // Must use the service tag
    public class PushHandlerService : GcmServiceBase
    {
        public static string RegistrationID { get; private set; }
        static NotificationHub Hub;

        public static Context Context;
		public static AzureNotifier MyAzureNotifier;

		public static void Initialize(Context context, AzureNotifier notifier)
        {
			MyAzureNotifier = notifier;
            PushHandlerService.Context = context;
            Console.WriteLine("PushHandlerService - Initialize - SenderIDS = " + MyBroadcastReceiver.SENDER_IDS.ToString());
            Console.WriteLine("PushHandlerService - Initialize - SenderID = " + AzureNotifier.SenderID);
            // Call this from our main activity
            Hub = new NotificationHub(AzureNotifier.NotificationHubPath, AzureNotifier.ConnectionString, context);
        }

        public static void Register(Context context)
        {
            PushHandlerService.Context = context;
            Console.WriteLine("PushHandlerService - Register");
            // Makes this easier to call from our Activity
            GcmClient.Register(context, AzureNotifier.SenderID);
        }

        protected override void OnError(Context context, string errorId)
        {
            PushHandlerService.Context = context;
			Console.WriteLine("PushHandlerService - OnError - errorId = " + errorId);
            Log.Info(MyBroadcastReceiver.LOG_CATEGORY, "OnError");
        }

        protected override void OnRegistered(Context context, string registrationId)
        {
            PushHandlerService.Context = context;
            Console.WriteLine("PushHandlerService - OnRegistered - registrationId = " + registrationId);
            Log.Info(MyBroadcastReceiver.LOG_CATEGORY, "OnRegistered");

            if (Hub != null)
                Hub.Register(registrationId, AzureNotifier.Tags.ToArray());
            Console.WriteLine("PushHandlerService - OnRegistered - Hub Tags = " + AzureNotifier.Tags.ToString());
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            PushHandlerService.Context = context;
            Console.WriteLine("PushHandlerService - OnUnRegistered - registrationId = " + registrationId);
            Log.Info(MyBroadcastReceiver.LOG_CATEGORY, "OnUnRegistered");


            if (Hub != null)
                Hub.Unregister();
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            PushHandlerService.Context = context;
            Console.WriteLine("PushHandlerService - OnMessage - intent = " + intent.ToString());
            Log.Info(MyBroadcastReceiver.LOG_CATEGORY, "OnMessage");
            if (intent != null)
            {
				HandleMessage(intent);
			//	string[] values = { "one", "two", "three" };
			//	Android.OS.BaseBundle extras = Android.OS.BaseBundle.FromArray<string>(values) as Android.OS.BaseBundle;

            }
            else
            {
                Log.Info(MyBroadcastReceiver.LOG_CATEGORY, "OnMessage::no intent");
            }
        }

		private void HandleMessage(Intent intent)
		{
			Log.Info(MyBroadcastReceiver.LOG_CATEGORY, "MESSAGE RECIEVED");
            // Custom logic... apply whatever you want
            // This logic happens when a push message is received

            // In this case, following statemens "transform" a push message into a notification message
            // In particular, through BuildNotificationIntent this method creates an interactive notification message
            // in other words, when user tap the notification, it activates an Intent and open an Activity
          //  string action = intent.Action;
         //  global::Android.OS.BaseBundle extras = intent.Extras;
            
			string message = "";//intent.Extras.GetString ("message");
			message = intent.Extras.ToString ();
			message = intent.Extras.GetString ("message");


//            Bundle[{from=737170756014, message=Booking Canceled, collapse_key=do_not_collapse}]

//Bundle[{from=737170756014, message=Booking Confirmed for user=C42AF4D3-2F6B-4008-B0C8-A7FEF0DC4A54 , at location=9F9B3379-B8CB-4474-BD22-8EE712A224E3, collapse_key=do_not_collapse}]

//Bundle[{from=737170756014, alert=test, message=Booking Confirmed for user=C42AF4D3-2F6B-4008-B0C8-A7FEF0DC4A54 , at location=00A398C7-621B-4D2D-AB4A-BF239E4EA428, collapse_key=do_not_collapse}]

        //    var msg = new StringBuilder();
        //    string myFld = null;
       //     if (intent.Extras != null)
       //     {
          //      foreach (string key in intent.Extras.KeySet().ToArray())
         //           msg.AppendLine(key + "=" + intent.Extras.Get(key));
		//		msg.AppendLine(intent.Extras.GetString("Message"));
        //        msg.AppendLine(intent.Extras.GetString("msg"));
        //        myFld = intent.Extras.GetString("customField2");
        //    }

       //     msg.AppendLine(!string.IsNullOrEmpty(action)
      //          ? string.Format("Intent action:{0}", action)
      //          : "Intent action:undefined");
            ///BuildNotificationIntent("OnMessage#3", message, false);
     //       Log.Info(MyBroadcastReceiver.LOG_CATEGORY, "OnMessage#3:{0}", msg.ToString());


            Context context = PushHandlerService.Context;

			var title = "New item added:";

			// Create a notification manager to send the notification.
			var notificationManager = 
				GetSystemService(Context.NotificationService) as NotificationManager;

			// Create a new intent to show the notification in the UI.   
		//	PendingIntent contentIntent = 
		//		PendingIntent.GetActivity(context, 0, 
		//			new Intent(this, typeof(ToDoActivity)), 0);           

			// Create the notification using the builder.
			var builder = new Notification.Builder(context);
			builder.SetAutoCancel(true);
			builder.SetContentTitle(title);
			builder.SetContentText(message);
		//	builder.SetSmallIcon(Resource.Drawable.ic_launcher);
			//builder.SetContentIntent(contentIntent);
			var notification = builder.Build();

			// Display the notification in the Notifications Area.
		//	notificationManager.Notify(1, notification);



			// Instantiate the builder and set notification elements:
			Notification.Builder builder1 = new Notification.Builder (this)
				.SetContentTitle ("OOOT Notification")
				.SetContentText (message)
                .SetContentIntent(AzureNotifier.PendingIntent)
				.SetDefaults(NotificationDefaults.Sound | NotificationDefaults.Vibrate)
				.SetSmallIcon (AzureNotifier.NotificationIcon);

			// Build the notification:
			Notification notification1 = builder1.Build();

			// Get the notification manager:
			NotificationManager notificationManager1 =
				GetSystemService (Context.NotificationService) as NotificationManager;

			// Publish the notification:
			const int notificationId = 0;
			notificationManager1.Notify (notificationId, notification1);

			if(MyAzureNotifier!=null)
			MyAzureNotifier.SendNotificationRecieved(new AzureNotificationEventArgs(message));

			Console.WriteLine("PushHandlerService - HandleMessage - completed message = " + message);
        }

        private void HandleRegistration(Intent intent)
        {
            Log.Info(MyBroadcastReceiver.LOG_CATEGORY, "REGISTRATION");
        }


        /// <summary>
        /// OnHandleIntent is just an higher level alternative to OnError, OnMessage, OnRegistered and OnUnRegistered
        /// 
        /// </summary>
        /// <param name="intent"></param>
        protected override void OnHandleIntent(Intent intent)
        {
            Log.Info(MyBroadcastReceiver.LOG_CATEGORY, "OnHandleIntent");
            if (intent != null)
            {
                string action = intent.Action;
                // Here you can put your custom logic
                if (action.Equals("com.google.android.c2dm.intent.REGISTRATION"))
                {
                    HandleRegistration(intent);
                }
                else if (action.Equals("com.google.android.c2dm.intent.MESSAGE"))
                {
                    HandleMessage(intent);
                }
                else
                {
                    Log.Info(MyBroadcastReceiver.LOG_CATEGORY, "OnHandleIntent::{0}", action);
                }
            }
            else
            {
                Log.Info(MyBroadcastReceiver.LOG_CATEGORY, "OnHandleIntent::no intent");
            }
            base.OnHandleIntent(intent);
        }

        private void BuildNotificationIntent(string title, string desc, bool openThird = false)
        {
            var notificationManager = GetSystemService(NotificationService) as NotificationManager;
            var notification = new Notification(Android.Resource.Drawable.SymActionEmail, title);
            notification.Flags = NotificationFlags.AutoCancel;
            Intent resultIntent = null;

            // The following statements control which activity open when user presses the notification
            ////if (openThird)
            ////{
            ////    resultIntent = new Intent(this, typeof(ThirdActivity));
            ////}
            ////else
            ////{
            ////    resultIntent = new Intent(this, typeof(NotificationViewActivity));
            ////}

            resultIntent.PutExtra("title", title);
            resultIntent.PutExtra("desc", desc);

            PendingIntent resultPendingIntent = PendingIntent.GetActivity(this, 1, resultIntent,
                PendingIntentFlags.OneShot);

            notification.SetLatestEventInfo(this, title, desc, resultPendingIntent);

            notificationManager.Notify(1, notification);
        }

    }

	public class ToDoActivity
	{
	// Create a new instance field for this activity.
	static ToDoActivity instance = new ToDoActivity();
		public MobileServiceClient client { get; private set; }
	// Return the current activity instance.
	public static ToDoActivity CurrentActivity
	{
		get
		{
			return instance;
		}
	}
	// Return the Mobile Services client.
	public MobileServiceClient CurrentClient
	{
		get
		{
			return client;
		}
	}
}

    ////////[BroadcastReceiver(Permission= "com.google.android.c2dm.permission.SEND")]
    ////////[IntentFilter(new string[] { "com.google.android.c2dm.intent.RECEIVE" }, Categories = new string[] {"@PACKAGE_NAME@" })]
    ////////[IntentFilter(new string[] { "com.google.android.c2dm.intent.REGISTRATION" }, Categories = new string[] {"@PACKAGE_NAME@" })]
    ////////[IntentFilter(new string[] { "com.google.android.gcm.intent.RETRY" }, Categories = new string[] { "@PACKAGE_NAME@"})]
    ////////public class MyGCMBroadcastReceiver : BroadcastReceiver
    ////////{
    ////////    const string TAG = "PushHandlerBroadcastReceiver";
    ////////    public override void OnReceive(Context context, Intent intent)
    ////////    {
    ////////        MyIntentService.RunIntentInService(context, intent);
    ////////        SetResult(Result.Ok, null, null);
    ////////    }
    ////////}


    ////////[BroadcastReceiver]
    ////////[IntentFilter(new string[] { "Android.Content.Intent.ActionBootCompleted" })]
    ////////public class MyGCMBootReceiver : BroadcastReceiver
    ////////{
    ////////    public override void OnReceive(Context context, Intent intent)
    ////////    {
    ////////        MyIntentService.RunIntentInService(context, intent);
    ////////        SetResult(Result.Ok, null, null);
    ////////    }
    ////////}




    ////////[Service]
    ////////public class MyIntentService : IntentService
    ////////{
    ////////    static PowerManager.WakeLock sWakeLock;
    ////////    static object LOCK = new object();

    ////////    internal static void RunIntentInService(Context context, Intent intent)
    ////////    {
    ////////        lock (LOCK)
    ////////        {
    ////////            if (sWakeLock == null)
    ////////            {
    ////////                // This is called from BroadcastReceiver, there is no init.
    ////////                var pm = PowerManager.FromContext(context);
    ////////                sWakeLock = pm.NewWakeLock(
    ////////                    WakeLockFlags.Partial, "My WakeLock Tag");
    ////////            }
    ////////        }

    ////////        sWakeLock.Acquire();
    ////////        intent.SetClass(context, typeof(MyIntentService));
    ////////        context.StartService(intent);
    ////////    }

    ////////    protected override void OnHandleIntent(Intent intent)
    ////////    {
    ////////        try
    ////////        {
    ////////            Context context = this.ApplicationContext;
    ////////            string action = intent.Action;

    ////////            if (action.Equals("com.google.android.c2dm.intent.REGISTRATION"))
    ////////            {
    ////////                HandleRegistration(context, intent);
    ////////            }
    ////////            else if (action.Equals("com.google.android.c2dm.intent.RECEIVE"))
    ////////            {
    ////////                HandleMessage(context, intent);
    ////////            }
    ////////        }
    ////////        finally
    ////////        {
    ////////            lock (LOCK)
    ////////            {
    ////////                //Sanity check for null as this is a public method
    ////////                if (sWakeLock != null)
    ////////                    sWakeLock.Release();
    ////////            }
    ////////        }
    ////////    }


    ////////    private void HandleRegistration(Context context, Intent intent)
    ////////    {
    ////////        Console.WriteLine("MyIntentService - HandleRegistration : " + intent.ToString());
    ////////           var status = GcmClient.IsRegistered(context);
    ////////           var regId = GcmClient.GetRegistrationId(context);
    ////////           AzureNotifier.RegistrationID = GcmClient.GetRegistrationId(context);


    ////////        Console.WriteLine("AzureGcmService - Registered GCM RegistrationID: " + AzureNotifier.RegistrationID);
    ////////    }


    ////////    private void HandleMessage(Context context, Intent intent)
    ////////    {
    ////////        Console.WriteLine("MyIntentService - HandleMessage : " + intent.ToString());

    ////////    }

    ////////}








//////////////    [BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
//////////////    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE }, Categories = new string[] { "@PACKAGE_NAME@" })]
//////////////    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new string[] { "@PACKAGE_NAME@" })]
//////////////    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new string[] { "@PACKAGE_NAME@" })]
//////////////    public class AzureBroadcastReceiver : GcmBroadcastReceiverBase<AzureGcmService>
//////////////    {
//////////////        public static string[] SENDER_IDS = new string[] { AzureNotifier.SenderID };

//////////////        public const string TAG = "BroadcastReceiver-GCM";

//////////////        //public override void OnReceive(Context context, Intent intent)
//////////////        //{
//////////////        //    Console.WriteLine("AzureBroadcastReceiver - OnReceive : " + intent.ToString());
//////////////        //    foreach (string key in intent.Extras.KeySet()) {
//////////////        //        Console.WriteLine("AzureBroadcastReceiver - intent key : " + key);
//////////////        //    }

//////////////        //    Console.WriteLine("AzureBroadcastReceiver - intent.GetStringExtra(data) : " + intent.GetStringExtra("data"));
//////////////        //    Console.WriteLine("AzureBroadcastReceiver - intent.GetStringExtra(title) : " + intent.GetStringExtra("title"));
//////////////        //    Console.WriteLine("AzureBroadcastReceiver - intent.GetStringExtra(message) : " + intent.GetStringExtra("title"));

//////////////        //    if (!string.IsNullOrEmpty(intent.GetStringExtra("title")))
//////////////        //    {
			
//////////////        //        NotificationCompat.Builder builder = new NotificationCompat.Builder(context)
//////////////        //            .SetContentTitle(intent.GetStringExtra("title"))
//////////////        //            .SetContentText(intent.GetStringExtra("message"));

//////////////        //        NotificationManager notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
//////////////        //        notificationManager.Notify(1, builder.Build());
//////////////        //    }

//////////////        ////    MyIntentService.RunIntentInService(context, intent);
//////////////        //    SetResult(Result.Ok, null, null);
//////////////        //}
//////////////    }





//////////////    [Service] //Must use the service tag
//////////////    public class AzureGcmService : GcmServiceBase
//////////////    {
//////////////        static NotificationHub Hub;
//////////////        static AzureNotifier AzureNotifier;

//////////////        public static void Initialize(AzureNotifier azureNotifier, Context context, string connectionString, string notificationHubName, string key)
//////////////        {
//////////////              Console.WriteLine("AzureGcmService - Initialize... ");
//////////////            AzureNotifier = azureNotifier;

//////////////            // Call this from our main activity
//////////////            //var cs = ConnectionString.CreateUsingSharedAccessKeyWithListenAccess(
//////////////            //    new Java.Net.URI(connectionString),
//////////////            //    key);

//////////////            var hubName = notificationHubName;

//////////////            Hub = new NotificationHub(notificationHubName, connectionString, context);

//////////////            Console.WriteLine("AzureGcmService - Initialize Complete ");
//////////////            //Hub = new NotificationHub(hubName, cs, context);
//////////////        }

//////////////        public static void Register(Context context)
//////////////        {
//////////////            // Check to ensure everything's setup right
//////////////            GcmClient.CheckDevice(context);
//////////////            GcmClient.CheckManifest(context);

//////////////            // Register for push notifications
//////////////            Console.WriteLine("AzureGcmService - Registering with GCM... ");
//////////////            System.Diagnostics.Debug.WriteLine("Registering with GCM...");
            
//////////////            // Makes this easier to call from our Activity
//////////////            GcmClient.Register(context, AzureNotifier.SenderID);
//////////////         //   var status = GcmClient.IsRegistered(context);
//////////////         //   var regId = GcmClient.GetRegistrationId(context);
//////////////         //   AzureNotifier.RegistrationID = GcmClient.GetRegistrationId(context);

////////////////			Thread thread = new Thread(new ThreadStart(RegisterHub));
////////////////			thread.Start();


////////////////			Console.WriteLine("AzureGcmService - Registered GCM RegistrationID: " + AzureNotifier.RegistrationID);
//////////////        }

//////////////        public AzureGcmService()
//////////////                : base(AzureBroadcastReceiver.SENDER_IDS)
//////////////        {
//////////////        }



//////////////        public static void RegisterHub()
//////////////        {

//////////////            Console.WriteLine("AzureGcmService - RegisterHub");
//////////////            try
//////////////            {
//////////////                if (!String.IsNullOrWhiteSpace(AzureNotifier.RegistrationID))
//////////////                {
//////////////                           Hub.UnregisterAll(AzureNotifier.RegistrationID);
//////////////                }
//////////////            }
//////////////            catch (Exception ex)
//////////////            {
//////////////                Console.WriteLine("AzureGcmService - RegisterHub OnRegistered: Error calling UnregisterAll: " + ex.Message);
//////////////                AzureNotifier.SendNotificationError(new AzureNotificationErrorEventArgs("OnRegistered: Error calling UnregisterAll: " + ex.Message));
//////////////                return;
//////////////            }

//////////////            try
//////////////            {
//////////////                Console.WriteLine("AzureGcmService - RegisterHub AzureNotifier.RegistrationID = " + AzureNotifier.RegistrationID);
//////////////                if (!String.IsNullOrWhiteSpace(AzureNotifier.RegistrationID))
//////////////                {
//////////////                           var hubRegistration = Hub.Register(AzureNotifier.RegistrationID, AzureNotifier.Tags);

//////////////                    Console.WriteLine("AzureGcmService - RegisterHub Success");
//////////////                    AzureNotifier.SendNotificationRecieved(new AzureNotificationEventArgs("Success", hubRegistration.RegistrationId));
//////////////                }
//////////////            }
//////////////            catch (Exception ex)
//////////////            {
//////////////                Console.WriteLine("AzureGcmService - RegisterHub OnRegistered: Error calling Register: " + ex.Message);
//////////////                AzureNotifier.SendNotificationError(new AzureNotificationErrorEventArgs("OnRegistered: Error calling Register: " + ex.Message));
//////////////                return;
//////////////            }
//////////////        }

//////////////        #region GCM Service Base overrides



//////////////        protected override void OnRegistered(Context context, string registrationId)
//////////////        {
//////////////            Console.WriteLine("AzureNotifier - OnRegistered : " + registrationId);
//////////////            AzureNotifier.RegistrationID = registrationId;
//////////////            if (Hub != null)
//////////////            {
//////////////                try
//////////////                {
//////////////                    if (!String.IsNullOrWhiteSpace(AzureNotifier.RegistrationID))
//////////////                    {
//////////////                        var hubRegistration = Hub.Register(AzureNotifier.RegistrationID, AzureNotifier.Tags);
//////////////                    }
//////////////                }
//////////////                catch (Exception ex)
//////////////                {
//////////////                    AzureNotifier.SendNotificationError(new AzureNotificationErrorEventArgs("OnRegistered: Error calling Register: " + ex.Message));
//////////////                    return;
//////////////                }
//////////////                //   Hub.Register(registrationId, "TEST");
//////////////            }
//////////////        }
//////////////        protected override void OnError(Context context, string errorId)
//////////////        {
//////////////            Console.WriteLine("AzureNotifier - OnError : " + errorId);
//////////////            AzureNotifier.SendNotificationError(new AzureNotificationErrorEventArgs("GCM Service Base Error [" + errorId + "]"));
//////////////        }
//////////////        protected override void OnMessage(Context context, Intent intent)
//////////////        {
//////////////            Console.WriteLine("AzureNotifier - OnMessage : " + intent.ToString());
//////////////            Log.Info("AzureNotifier", "GCM Message Received!");

//////////////            var msg = new StringBuilder();

//////////////            if (intent != null && intent.Extras != null)
//////////////            {
//////////////                foreach (var key in intent.Extras.KeySet())
//////////////                    msg.AppendLine(key + "=" + intent.Extras.Get(key).ToString());
//////////////            }

//////////////            string messageText = intent.Extras.GetString("msg");
//////////////            if (!string.IsNullOrEmpty(messageText))
//////////////            {
//////////////                AzureNotifier.SendNotificationRecieved(new AzureNotificationEventArgs(messageText));
//////////////                return;
//////////////            }

//////////////            AzureNotifier.SendNotificationRecieved(new AzureNotificationEventArgs(msg.ToString()));
//////////////        }
//////////////        protected override void OnUnRegistered(Context context, string registrationId)
//////////////        {
//////////////            Console.WriteLine("AzureNotifier - OnUnRegistered : " + registrationId);
//////////////            if (Hub != null)
//////////////                Hub.Unregister();
//////////////        }
//////////////        protected override bool OnRecoverableError(Context context, string errorId)
//////////////        {
//////////////            Console.WriteLine("AzureNotifier - OnRecoverableError : " + errorId);
//////////////            //Some recoverable error happened
//////////////            return true;
//////////////        }


//////////////        protected override void OnHandleIntent(Intent intent)
//////////////        {
//////////////            Console.WriteLine("AzureNotifier - OnHandleIntent : " + intent.ToString());
//////////////            Log.Info("AzureNotifier", "OnHandleIntent : " + intent.ToString());
//////////////            //	base.OnHandleIntent (intent);

//////////////        //	foreach (string cat in intent.Categories) {
//////////////        //		Console.WriteLine("AzureNotifier - intent Category : " + cat);
//////////////        //	}
//////////////        //	var extras = intent.Extras;
//////////////        //	Console.WriteLine("AzureNotifier - extras : " + extras.ToString());
//////////////        //	foreach (string key in extras.KeySet()) {
//////////////        //		Console.WriteLine("AzureNotifier - intent key : " + key);
//////////////        //	}
//////////////        }

//////////////        #endregion
//////////////    }



}