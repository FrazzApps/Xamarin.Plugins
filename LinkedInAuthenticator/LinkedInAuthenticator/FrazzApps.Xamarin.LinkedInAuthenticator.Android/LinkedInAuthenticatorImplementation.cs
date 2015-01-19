using FrazzApps.Xamarin.LinkedInAuthenticator.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.LinkedInAuthenticator.Android;

[assembly: Dependency(typeof(LinkedInAuthenticatorImplementation))]
namespace FrazzApps.Xamarin.LinkedInAuthenticator.Android
{
    /// <summary>
    /// LinkedInAuthenticator Implementation
    /// </summary>
    public class LinkedInAuthenticatorImplementation : ILinkedInAuthenticator
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { }
    }
}
