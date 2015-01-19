using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FrazzApps.Xamarin.IconGenerator
{
    public class Icon : Label  {
        public Icon()
        {
            this.Font = Device.OnPlatform(
                                    Font.OfSize("FontAwesome", NamedSize.Large),
                                    Font.OfSize("FontAwesome", NamedSize.Large), // will get overridden in custom Renderer
                                    Font.OfSize(@"\Assets\Fonts\fontawesome-webfont.ttf#FontAwesome", NamedSize.Large)
                                );
            this.VerticalOptions = LayoutOptions.Center;
            this.HorizontalOptions = LayoutOptions.Center;
        }

        //http://fortawesome.github.io/Font-Awesome/cheatsheet/  
    }

}
