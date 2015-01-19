using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrazzApps.Xamarin.FacebookAuthenticator
{
    public class FacebookAuthenticationEventArgs : EventArgs
    {
        public FacebookAuthenticationEventArgs(string code, string token, DateTime expiry)
        {
            this.AccessCode = code;
            this.AccessToken = token;
            this.AccessTokenExpiry = expiry;
        }

        public string AccessCode { get; private set; }
        public string AccessToken { get; private set; }
        public DateTime AccessTokenExpiry { get; private set; }
    }
}
