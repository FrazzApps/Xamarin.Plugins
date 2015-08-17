using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using FrazzApps.Xamarin.AppRater.Abstractions;

namespace FrazzApps.Xamarin.AppRater
{
    public class RateFeedbackDialog
    {

        public string RateMessageTitle { get; set; }
        public string RateMessage { get; set; }
        public string RateButtonLabel { get; set; }
        public string RateCancelLabel { get; set; }

        public string FeebackMessageTitle { get; set; }
        public string FeebackMessage { get; set; }
        public string FeebackButtonLabel { get; set; }
        public string FeebackCancelLabel { get; set; }


        public string FeedbackEmail { get; set; }
        public string FeedbackSubject { get; set; }
        public string FeedbackBody { get; set; }
        public string AppId { get; set; }
        public string AppName { get; set; }

        public RateFeedbackDialog(string feedbackEmail, string appName, string appId)
        {
            AppId = appId;
            AppName = appName;

            RateMessageTitle = string.Format("Enjoying {0}?", appName);
            RateMessage = "We'd love you to rate our app 5 stars.\n Showing us some love on the store helps us to contrinue to work on the app and make things better!";
            RateButtonLabel = "rate 5 stars";
            RateCancelLabel = "no thanks";

            FeebackMessageTitle = "Can we make it better?";
            FeebackMessage = string.Format("Sorry to hear you didn't want to rate {0}.\n Tell us about your experience or suggest how we can make it even better.", appName);
            FeebackButtonLabel = "give feedback";
            FeebackCancelLabel = "no thanks";

            FeedbackEmail = feedbackEmail;
            FeedbackSubject = string.Format("{0} Feedback", appName);
            FeedbackBody = "";
        }

        public event EventHandler<RateFeedbackEventArgs> RateFeedbackCompleted;


        public async void Show(Page callingPage)
        {
            RateFeedbackResult result = RateFeedbackResult.None;

            await Task.Delay(1);
     
           // var rateAnswer = await callingPage.DisplayAlert(RateMessageTitle, RateMessage, RateButtonLabel, RateCancelLabel);

            callingPage.DisplayAlert(RateMessageTitle, RateMessage, RateButtonLabel, RateCancelLabel).ContinueWith(r =>
            {
                if (r.Result)
                {
                    IAppRater rater = DependencyService.Get<IAppRater>();
                    rater.RateApp(this.AppId);
                    result = RateFeedbackResult.Rate;

                    this.RateFeedbackCompleted(callingPage, new RateFeedbackEventArgs(result));
                }
                else
                {
                  //  var feedbackAnswer = await callingPage.DisplayAlert(FeebackMessageTitle, FeebackMessage, FeebackButtonLabel, FeebackCancelLabel);
                    callingPage.DisplayAlert(FeebackMessageTitle, FeebackMessage, FeebackButtonLabel, FeebackCancelLabel).ContinueWith(f =>
                    {
                        if (f.Result)
                        {
                            IEmailComposer emailer = DependencyService.Get<IEmailComposer>();
                            emailer.SendEmail(FeedbackEmail, FeedbackSubject, FeedbackBody);
                            result = RateFeedbackResult.Feedback;

                            this.RateFeedbackCompleted(callingPage, new RateFeedbackEventArgs(result));
                        }
                        else
                        {
                            this.RateFeedbackCompleted(callingPage, new RateFeedbackEventArgs(result));
                        }
                    }, TaskScheduler.FromCurrentSynchronizationContext()
                    );
                }
            }, TaskScheduler.FromCurrentSynchronizationContext()
            ); 

        }
    }

}
