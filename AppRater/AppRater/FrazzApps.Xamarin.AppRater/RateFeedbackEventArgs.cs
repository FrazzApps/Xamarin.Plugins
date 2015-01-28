using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrazzApps.Xamarin.AppRater
{
    public class RateFeedbackEventArgs : EventArgs
    {
        public RateFeedbackEventArgs(RateFeedbackResult result)
        { this.Result = result; }

        public RateFeedbackResult Result { get; private set; }
    }

    public enum RateFeedbackResult
    {
        Rate,
        Feedback,
        None
    }
}
