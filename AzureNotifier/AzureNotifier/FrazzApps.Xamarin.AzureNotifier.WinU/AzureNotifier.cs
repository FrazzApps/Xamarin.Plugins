using FrazzApps.Xamarin.AzureNotifier.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.AzureNotifier.WinU;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.Messaging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Networking.PushNotifications;

[assembly: Dependency(typeof(AzureNotifier))]
namespace FrazzApps.Xamarin.AzureNotifier.WinU
{
  /// <summary>
  /// FrazzApps.Xamarin.AzureNotifier Implementation
  /// </summary>
  public class AzureNotifier : IAzureNotifier
  {
    /// <summary>
    /// Used for registration with dependency service
    /// </summary>
      public static void Init() { }
      public AzureNotifier() { }

      public event EventHandler<AzureNotificationEventArgs> NotificationRecieved;
      public event EventHandler<AzureNotificationErrorEventArgs> NotificationError;
      public event EventHandler<AzureNotificationEventArgs> NotificationRegistered;

      #region Push Notifications


      public static PushNotificationChannel CurrentChannel { get; private set; }

      public async void RegisterForRemotePushNotifications(MobileServiceClient client, string channelName)
      {
          CurrentChannel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
          CurrentChannel.PushNotificationReceived += CurrentChannel_PushNotificationReceived; 
       
      }

        private void CurrentChannel_PushNotificationReceived(PushNotificationChannel sender, PushNotificationReceivedEventArgs args)
        {
            string message = "";

            switch (args.NotificationType)
            {
                case PushNotificationType.Badge:
                    message = args.BadgeNotification.Content.InnerText;
                    break;
                case PushNotificationType.Raw:
                    message = args.RawNotification.Content;
                    break;
                case PushNotificationType.Tile:
                    message = args.TileNotification.Content.InnerText;
                    break;
                case PushNotificationType.TileFlyout:
                    message = args.TileNotification.Content.InnerText;
                    break;
                case PushNotificationType.Toast:
                    message = args.ToastNotification.Content.InnerText;
                    break;
            }
            if (this.NotificationRecieved != null)
                this.NotificationRecieved(this, new AzureNotificationEventArgs(message));
        }
        #endregion


        #region Notification Hubs
        private NotificationHub Hub;
      public async void RegisterForRemoteHubNotifications(string notificationHubName, string connectionString, string channelName, string[] tags)
      {
            
         var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
         
          //channel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(async (o, args) =>
          //{
          //    Hub = new NotificationHub(notificationHubName, connectionString);
          //    Microsoft.WindowsAzure.Messaging.Registration registration = await Hub.RegisterNativeAsync(args.ChannelUri.ToString(), tags);
          //    if (this.NotificationRegistered!=null)
          //    this.NotificationRegistered(this, new AzureNotificationEventArgs("Success", registration.RegistrationId));
          //    System.Diagnostics.Debug.WriteLine("RegisterForRemoteHubNotifications completed");
          //});
      }

////////      public async Task<string> RequestRegistrationId(string notificationHubName, string connectionString, string handle = null)
////////      {
          
          
////////// Microsoft.WindowsAzure.MobileServices.MpnsRegistration reg = new MpnsRegistration(;
//////////using Microsoft.WindowsAzure.Messaging;

////////          // make sure there are no existing registrations for this push handle (used for iOS and Android)
////////          string newRegistrationId = null;

////////          if (handle != null)
////////          {
////////              var registrations = await Hub.GetRegistrationsByChannelAsync(handle, 100);

////////              foreach (RegistrationDescription registration in registrations)
////////              {
////////                  if (newRegistrationId == null)
////////                  {
////////                      newRegistrationId = registration.RegistrationId;
////////                  }
////////                  else
////////                  {
////////                      await Hub.DeleteRegistrationAsync(registration);
////////                  }
////////              }
////////          }

////////          if (newRegistrationId == null) newRegistrationId = await Hub.CreateRegistrationIdAsync();

////////          return newRegistrationId;
////////      }

      #endregion
  }
}
