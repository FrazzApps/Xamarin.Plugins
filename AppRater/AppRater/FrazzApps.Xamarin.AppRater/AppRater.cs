using FrazzApps.Xamarin.AppRater.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FrazzApps.Xamarin.AppRater
{
    public class AppRater
    {
        public static IAppRater Instance
        {
            get { return DependencyService.Get<IAppRater>(); }
        }
    }
}
