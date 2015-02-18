using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FrazzApps.Xamarin.GoogleAnalyticsConnector
{
    internal static class HttpWebHelper
    {
            internal static Task<WebResponse> GetResponseAsync(HttpWebRequest request)
            {
                if (request == null) throw new ArgumentNullException("request");
                return Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null);
            }

            internal static Dictionary<string, string> ParseQueryString(Uri uri)
            {
                var query = uri.Query.Substring(uri.Query.IndexOf('?') + 1); // +1 for skipping '?'
                return HttpWebHelper.ParseQueryString(query);
            }

            internal static Dictionary<string, string> ParseQueryString(string queryString)
            {
                var pairs = queryString.Split('&');
                return pairs
                    .Select(o => o.Split('='))
                    .Where(items => items.Count() == 2)
                    .ToDictionary(pair => Uri.UnescapeDataString(pair[0]),
                        pair => Uri.UnescapeDataString(pair[1]));
            }
        
    }
}
