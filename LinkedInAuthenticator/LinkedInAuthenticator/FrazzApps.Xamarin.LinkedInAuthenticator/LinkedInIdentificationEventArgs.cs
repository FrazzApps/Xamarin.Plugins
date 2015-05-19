using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrazzApps.Xamarin.LinkedInAuthenticator
{
    public class LinkedInIdentificationEventArgs : EventArgs
    {

		public LinkedInIdentificationEventArgs(LinkedInAuthenticationEventArgs authorization, string results)
		{
			this.AuthenticationEventArgs = authorization;
			this.Result = results;
		}

        public LinkedInIdentificationEventArgs(string code, string token, DateTime expiry, string results)
        {
			this.AuthenticationEventArgs = new LinkedInAuthenticationEventArgs(code, token, expiry);
			this.Result = results;
        }

        public LinkedInAuthenticationEventArgs AuthenticationEventArgs { get; private set; }
		public string Result { get; private set; }
    }
}
