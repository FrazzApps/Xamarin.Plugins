using FrazzApps.Xamarin.FacebookAuthenticator.Abstractions;
using System;
using Xamarin.Forms;
using FacebookAuthenticator.Forms.Plugin.WinU;

[assembly: Dependency(typeof(FacebookAuthenticatorImplementation))]
namespace FacebookAuthenticator.Forms.Plugin.WinU
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
