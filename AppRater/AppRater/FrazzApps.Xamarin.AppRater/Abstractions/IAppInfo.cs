using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrazzApps.Xamarin.AppRater.Abstractions
{
    public interface IAppInfo
    {
        string AppName { get; }
        string AppId { get; }
    }
}
