using FrazzApps.Xamarin.LinkedInAuthenticator.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.LinkedInAuthenticator.iOS;

[assembly: Dependency(typeof(LinkedInAuthenticatorImplementation))]
namespace FrazzApps.Xamarin.LinkedInAuthenticator.iOS
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
