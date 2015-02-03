using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrazzApps.Xamarin.CalendarConnector
{
    public enum AppointmentReminder
    {
        none,
        five,
        fifteen,
        thirty
    }

    public enum AppointmentStatus
    {
        busy,
        free,
        tentative,
        outofoffice
    }
}
