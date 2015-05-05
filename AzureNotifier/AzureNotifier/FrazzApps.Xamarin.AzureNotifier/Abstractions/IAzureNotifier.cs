using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;

namespace FrazzApps.Xamarin.AzureNotifier.Abstractions
{
    /// <summary>
    /// FrazzApps.Xamarin.AzureNotifier Interface
    /// </summary>
    public interface IAzureNotifier
    {

        event EventHandler<AzureNotificationEventArgs> NotificationRecieved;
        event EventHandler<AzureNotificationErrorEventArgs> NotificationError;
        event EventHandler<AzureNotificationEventArgs> NotificationRegistered;


        #region Push Notifications
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters">DeviceToken|ChannelName</param>
        /// <param name="client"></param>
        void RegisterForRemotePushNotifications(MobileServiceClient client, string channelName);

        #endregion


        #region Notification Hubs
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters">DeviceToken|ChannelName|connectionString|notificationHubPath</param>
        void RegisterForRemoteHubNotifications(string notificationHubName, string connectionString, string channelName, string[] tags);

        #endregion

    }
}
