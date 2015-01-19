using FrazzApps.Xamarin.FacebookAuthenticator.Abstractions;
using System;
using Xamarin.Forms;
using FacebookAuthenticator.Forms.Plugin.iOSUnified;

[assembly: Dependency(typeof(FacebookAuthenticatorImplementation))]
namespace FacebookAuthenticator.Forms.Plugin.iOSUnified
{
    /// <summary>
    /// FacebookAuthenticator Implementation
    /// </summary>
    public class FacebookAuthenticatorImplementation : IFacebookAuthenticator
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { }
    }
}
