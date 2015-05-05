using System;

namespace FrazzApps.Xamarin.AppInformer.Abstractions
{
    /// <summary>
    /// AppInformer Interface
    /// </summary>
    public interface IAppInformer
    {
        /// <summary>
        /// Get the name of the App
        /// </summary>
        string AppName { get; }

        /// <summary>
        /// Get the version of the App
        /// </summary>
        string AppVersion { get; }

        /// <summary>
        /// Get the Id of the App
        /// </summary>
        string AppId { get; }

        /// <summary>
        /// Get the Installer Id of the App
        /// </summary>
        string AppInstallerId { get; }
    }
}
