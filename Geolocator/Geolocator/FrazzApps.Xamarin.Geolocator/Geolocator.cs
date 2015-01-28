using FrazzApps.Xamarin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FrazzApps.Xamarin.Geolocator
{
    public class Geolocator
    {

        public static IGeolocator Instance
        {
            get { return DependencyService.Get<IGeolocator>(); }
        }

    }
}
