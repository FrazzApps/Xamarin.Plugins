using FrazzApps.Xamarin.AzureNotifier.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.AzureNotifier.WinPhone;
using Microsoft.Phone.Notification;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.Messaging;
using System.Collections.Generic;
using System.Threading.Tasks;

[assembly: Dependency(typeof(AzureNotifier))]
namespace FrazzApps.Xamarin.AzureNotifier.WinPhone
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


      public static HttpNotificationChannel CurrentChannel { get; private set; }

      public void RegisterForRemotePushNotifications(MobileServiceClient client, string channelName)
      {
          CurrentChannel = HttpNotificationChannel.Find(channelName);
          if (CurrentChannel == null)
          {
              CurrentChannel = new HttpNotificationChannel(channelName);
              CurrentChannel.Open();
              CurrentChannel.BindToShellToast();
          }
          CurrentChannel.ChannelUriUpdated +=
              new EventHandler<NotificationChannelUriEventArgs>(async (o, args) =>
              {
                  // Register for notifications using the new channel
                  System.Exception exception = null;
                  try
                  {
                      await client.GetPush()
                          .RegisterNativeAsync(CurrentChannel.ChannelUri.ToString());
                  }
                  catch (System.Exception ex)
                  {
                      CurrentChannel.Close();
                      exception = ex;
                  }
                  if (exception != null)
                  {
                      if (this.NotificationError != null)
                      this.NotificationError(this, new AzureNotificationErrorEventArgs(exception.Message));
                  }
              });
          CurrentChannel.ShellToastNotificationReceived +=
              new EventHandler<NotificationEventArgs>((o, args) =>
              {
                  string message = "";
                  foreach (string key in args.Collection.Keys)
                  {
                      message += key + " : " + args.Collection[key] + ", ";
                  }
                  if (this.NotificationRecieved != null)
                  this.NotificationRecieved(this, new AzureNotificationEventArgs(message));
              });
      }
      #endregion


      #region Notification Hubs
      private NotificationHub Hub;
      public void RegisterForRemoteHubNotifications(string notificationHubName, string connectionString, string channelName, string[] tags)
      {

          var channel = HttpNotificationChannel.Find(channelName);
          if (channel == null)
          {
              channel = new HttpNotificationChannel(channelName);
              channel.Open();
              channel.BindToShellToast();
          }

          channel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(async (o, args) =>
          {
              Hub = new NotificationHub(notificationHubName, connectionString);
              Microsoft.WindowsAzure.Messaging.Registration registration = await Hub.RegisterNativeAsync(args.ChannelUri.ToString(), tags);
              if (this.NotificationRegistered!=null)
              this.NotificationRegistered(this, new AzureNotificationEventArgs("Success", registration.RegistrationId));
              Console.WriteLine("RegisterForRemoteHubNotifications completed");
          });
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
