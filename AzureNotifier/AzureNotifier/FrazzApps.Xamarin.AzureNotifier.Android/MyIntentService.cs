using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FrazzApps.Xamarin.AzureNotifier.Android
{

    [Service]
    public class MyIntentService : IntentService
    {
        static PowerManager.WakeLock sWakeLock;
        static object LOCK = new object();

        public static void RunIntentInService(Context context, Intent intent)
        {
            lock (LOCK)
            {
                if (sWakeLock == null)
                {
                    // This is called from BroadcastReceiver, there is no init.
                    var pm = PowerManager.FromContext(context);
                    sWakeLock = pm.NewWakeLock(
                    WakeLockFlags.Partial, "My WakeLock Tag");
                }
            }

            sWakeLock.Acquire();
            intent.SetClass(context, typeof(MyIntentService));
            context.StartService(intent);
        }

        protected override void OnHandleIntent(Intent intent)
        {
            try
            {
                Context context = this.ApplicationContext;
                string action = intent.Action;

                if (action.Equals("com.google.android.c2dm.intent.REGISTRATION"))
                {
                    //TODO: HandleRegistration(context, intent);
                }
                else if (action.Equals("com.google.android.c2dm.intent.RECEIVE"))
                {
                  //TODO:  HandleMessage(context, intent);
                }
            }
            finally
            {
                lock (LOCK)
                {
                    //Sanity check for null as this is a public method
                    if (sWakeLock != null)
                        sWakeLock.Release();
                }
            }
        }
    }
}