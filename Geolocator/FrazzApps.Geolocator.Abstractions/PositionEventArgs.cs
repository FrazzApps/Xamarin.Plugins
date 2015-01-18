using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrazzApps.Geolocator.Abstractions
{
    public class PositionEventArgs : EventArgs
    {
        public PositionEventArgs(Position position)
        { this.Position = position; }

        public Position Position { get; private set; }
    }
}
