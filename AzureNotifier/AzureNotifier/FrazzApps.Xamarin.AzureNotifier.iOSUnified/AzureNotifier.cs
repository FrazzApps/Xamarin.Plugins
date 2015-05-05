using FrazzApps.Xamarin.AzureNotifier.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.AzureNotifier.iOSUnified;
using UIKit;
using Foundation;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using WindowsAzure.Messaging;

[assembly: Dependency(typeof(AzureNotifier))]
namespace FrazzApps.Xamarin.AzureNotifier.iOSUnified
{
    /// <summary>
    /// FrazzApps.Xamarin.AzureNotifier Implementation
    /// </summary>
    public class AzureNotifier : IAzureNotifier
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init(NSData deviceToken)
        {
            DeviceToken = deviceToken;
            DeviceTokenString = deviceToken.Description.Trim('<', '>').Replace(" ", "");
            RegisterSettings();
        }
        public AzureNotifier() { }

        public event EventHandler<AzureNotificationEventArgs> NotificationRecieved;
        public event EventHandler<AzureNotificationErrorEventArgs> NotificationError;
        public event EventHandler<AzureNotificationEventArgs> NotificationRegistered;

        public static NSData DeviceToken { get; set; }
        public static string DeviceTokenString { get; set; }

        public static void RegisterSettings()
        {
            // registers for push for iOS8
            var settings = UIUserNotificationSettings.GetSettingsForTypes(
                UIUserNotificationType.Alert |
                UIUserNotificationType.Badge |
                UIUserNotificationType.Sound,
                new NSSet());

            UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            UIApplication.SharedApplication.RegisterForRemoteNotifications();


            UIRemoteNotificationType notificationTypes =
                UIRemoteNotificationType.Alert |
                UIRemoteNotificationType.Badge |
                UIRemoteNotificationType.Sound;

            UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
        }


        #region Push Notifications


        public void RegisterForRemotePushNotifications(MobileServiceClient client, string channelName)
        {
            // Register for push with Mobile Services
            IEnumerable<string> tag = new List<string>() { channelName };
            var push = client.GetPush();
            push.RegisterNativeAsync(DeviceToken, tag);
        }



        public void ReceivedRemoteNotification(NSDictionary userInfo)
        {
            NSObject inAppMessage;

            bool success = userInfo.TryGetValue(new NSString("inAppMessage"), out inAppMessage);

            if (success)
            {
                if (this.NotificationRecieved != null)
                this.NotificationRecieved(this, new AzureNotificationEventArgs(inAppMessage.ToString()));
            }
        }

        #endregion


        #region Notification Hubs
        private SBNotificationHub Hub { get; set; }



        public void RegisterForRemoteHubNotifications(string notificationHubName, string connectionString, string channelName, string[] tags)
        {

            Hub = new SBNotificationHub(connectionString, notificationHubName);

            Hub.UnregisterAllAsync(DeviceToken, (error) =>
            {
                if (error != null)
                {
                    if (this.NotificationError != null)
                    this.NotificationError(this, new AzureNotificationErrorEventArgs("Error calling Unregister: " + error.ToString()));
                    return;
                }
                
               //// NSSet tags = null; // create tags if you want
                Hub.RegisterNativeAsync(DeviceToken, new NSSet(tags), (errorCallback) =>
                {
                    if (errorCallback != null)
                    {
                        if (this.NotificationError != null)
                        this.NotificationError(this, new AzureNotificationErrorEventArgs("RegisterNativeAsync error: " + errorCallback.ToString()));
                        Console.WriteLine("RegisterNativeAsync error: " + errorCallback.ToString());
                    }
                });
            });
        }


        //public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        //{
        //    ProcessNotification(userInfo, false);
        //}

        void ProcessNotification(NSDictionary options, bool fromFinishedLaunching)
        {
            // Check to see if the dictionary has the aps key.  This is the notification payload you would have sent
            if (null != options && options.ContainsKey(new NSString("aps")))
            {
                //Get the aps dictionary
                NSDictionary aps = options.ObjectForKey(new NSString("aps")) as NSDictionary;

                string alert = string.Empty;

                //Extract the alert text
                // NOTE: If you're using the simple alert by just specifying 
                // "  aps:{alert:"alert msg here"}  " this will work fine.
                // But if you're using a complex alert with Localization keys, etc., 
                // your "alert" object from the aps dictionary will be another NSDictionary. 
                // Basically the json gets dumped right into a NSDictionary, 
                // so keep that in mind.
                if (aps.ContainsKey(new NSString("alert")))
                    alert = (aps[new NSString("alert")] as NSString).ToString();

                //If this came from the ReceivedRemoteNotification while the app was running,
                // we of course need to manually process things like the sound, badge, and alert.
                if (!fromFinishedLaunching)
                {
                    //Manually show an alert
                    if (!string.IsNullOrEmpty(alert))
                    {
                        if (this.NotificationRecieved != null)
                        this.NotificationRecieved(this, new AzureNotificationEventArgs(alert));
                        // UIAlertView avAlert = new UIAlertView("Notification", alert, null, "OK", null);
                        // avAlert.Show();
                    }
                }
            }
        }

        #endregion
    }
}
