using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrazzApps.Xamarin.FacebookAuthenticator
{
    public class FacebookIdentificationEventArgs : EventArgs
    {

		public FacebookIdentificationEventArgs(FacebookAuthenticationEventArgs authorization, string results)
		{
			this.AuthenticationEventArgs = authorization;
			this.Result = results;
		}

		public FacebookIdentificationEventArgs(string code, string token, DateTime expiry, string results)
        {
			this.AuthenticationEventArgs = new FacebookAuthenticationEventArgs(code, token, expiry);
			this.Result = results;
        }

		public FacebookAuthenticationEventArgs AuthenticationEventArgs { get; private set; }
		public string Result { get; private set; }
    }
}
