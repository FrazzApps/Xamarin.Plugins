using FrazzApps.Xamarin.UrlOpener.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FrazzApps.Xamarin.UrlOpener
{
    public class UrlLabel : Label
    {
        public UrlLabel()
        {
            TapGestureRecognizer tapGesture = new TapGestureRecognizer() { NumberOfTapsRequired = 1 };
            tapGesture.Tapped += Tapped;
            this.GestureRecognizers.Add(tapGesture);
        }

        private void Tapped(object sender, EventArgs e)
        {
            IUrlOpener opener = DependencyService.Get<IUrlOpener>();
            opener.Open(this.Text);
        }

    }
}
