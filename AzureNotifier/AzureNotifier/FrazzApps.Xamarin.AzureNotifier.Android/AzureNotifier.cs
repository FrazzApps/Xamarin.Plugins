using FrazzApps.Xamarin.AzureNotifier.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.AzureNotifier.Android;
using Gcm;
using Android.Util;
using WindowsAzure.Messaging;
using Android.OS;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using Android.Support.V4.App;
using System.Threading.Tasks;
using System.Threading;

[assembly: Dependency(typeof(AzureNotifier))]
namespace FrazzApps.Xamarin.AzureNotifier.Android
{

  /// <summary>
  /// FrazzApps.Xamarin.AzureNotifier Implementation
  /// </summary>
  public class AzureNotifier : IAzureNotifier
  {
    /// <summary>
    /// Used for registration with dependency service
    /// </summary>
		public static void Init(string deviceID, string googleProjectNumber, string registrationId, int notificationIcon, PendingIntent pendingIntent)
      { SenderID = googleProjectNumber; DeviceID = deviceID; RegistrationID = registrationId; NotificationIcon = notificationIcon; PendingIntent = pendingIntent; }


      public event EventHandler<AzureNotificationEventArgs> NotificationRecieved;
      public event EventHandler<AzureNotificationErrorEventArgs> NotificationError;
      public event EventHandler<AzureNotificationEventArgs> NotificationRegistered;

      public static string[] SENDER_IDS = new string[] { AzureNotifier.SenderID };
      public static string SenderID = "<GoogleProjectNumber>"; // Google API Project Number
      public static string RegistrationID = "";
      public static string DeviceID = "";
		public static string Channel = "";
		public static string[] Tags;
		public static int NotificationIcon;
        public static PendingIntent PendingIntent;

  //    private NotificationHub Hub { get; set; }



      #region Push Notifications
      public void RegisterForRemotePushNotifications(MobileServiceClient client, string channelName)
      {
          // Register for push with Mobile Services
          IEnumerable<string> tag = new List<string>() { channelName };
          var push = client.GetPush();
          push.RegisterNativeAsync(DeviceID, tag);         
      }
      #endregion


      #region Notification Hubs

    //  NotificationHub Hub;

      // Azure app specific connection string and hub path
      public static string ConnectionString = "<Azure connection string>";
      public static string NotificationHubPath = "<hub path>";


      public void RegisterForRemoteHubNotifications(string notificationHubName, string connectionString, string channelName, string[] tags)
		{    
          AzureNotifier.ConnectionString = connectionString;
          AzureNotifier.NotificationHubPath = notificationHubName;
			AzureNotifier.Tags = tags;
			AzureNotifier.Channel = channelName;


            Console.WriteLine("RegisterForRemoteHubNotifications - PushHandlerService Initialize... ");
            PushHandlerService.Initialize(Forms.Context, this);
            Console.WriteLine("RegisterForRemoteHubNotifications - PushHandlerService Register... ");
            PushHandlerService.Register(Forms.Context);


    ////////// Initialize Gcm Service Hub
    ////////   //     AzureGcmService.Initialize(this, Forms.Context, connectionString, notificationHubName, "");
              
    ////////// Register for GCM
    ////////   //     AzureGcmService.Register(Forms.Context);
		
    ////////        Hub = new NotificationHub(AzureNotifier.NotificationHubPath, AzureNotifier.ConnectionString, Forms.Context);

    ////////   //     Thread thread = new Thread(new ThreadStart(Register));

    ////////  //      thread.Start();
    ////////        GcmClient.CheckDevice(Forms.Context);
    ////////        GcmClient.CheckManifest(Forms.Context);
    ////////        Console.WriteLine("AzureGcmService - UnRegister with GCM... ");
    ////////        GcmClient.UnRegister (Forms.Context);
    ////////        Console.WriteLine("AzureGcmService - Registering with GCM... ");
    ////////        GcmClient.Register(Forms.Context, AzureNotifier.SenderID);
    ////////        var status = GcmClient.IsRegistered(Forms.Context);
    ////////        Console.WriteLine("AzureGcmService - GcmClient.IsRegistered : " + status);
    ////////        var regId = GcmClient.GetRegistrationId(Forms.Context);
    ////////        Console.WriteLine("AzureGcmService - GcmClient.GetRegistrationId : " + regId);
        }

		public void Register()
		{
			
            try
            {
                if (!String.IsNullOrWhiteSpace(AzureNotifier.RegistrationID))
                {
          //          Hub.UnregisterAll(AzureNotifier.RegistrationID);
                }
            }
            catch (Exception ex)
            {
                if (this.NotificationError != null)
                this.NotificationError(this, new AzureNotificationErrorEventArgs("Error calling UnregisterAll: " + ex.Message));
                return;
            }

            try
            {
                if (!String.IsNullOrWhiteSpace(AzureNotifier.RegistrationID))
                {
             //       var hubRegistration = Hub.Register(AzureNotifier.RegistrationID, AzureNotifier.Tags);

               //     if (this.NotificationRegistered != null)
               //     this.NotificationRegistered(this, new AzureNotificationEventArgs("Success", hubRegistration.RegistrationId));
                }
            }
            catch (Exception ex)
            {
                if (this.NotificationError != null)
                this.NotificationError(this, new AzureNotificationErrorEventArgs("Error calling Register: " + ex.Message));
                return;
            }
		}


        public void SendNotificationError(AzureNotificationErrorEventArgs message)
        {
            if (this.NotificationError != null)
                this.NotificationError(this, message);
        }

        public void SendNotificationRecieved(AzureNotificationEventArgs message)
        {
            if (this.NotificationRecieved != null)
                this.NotificationRecieved(this, message);
        }

        public void SendNotificationRegistered(AzureNotificationEventArgs message)
        {
            if (this.NotificationRegistered != null)
                this.NotificationRegistered(this, message);
        }
      #endregion

  }
}
