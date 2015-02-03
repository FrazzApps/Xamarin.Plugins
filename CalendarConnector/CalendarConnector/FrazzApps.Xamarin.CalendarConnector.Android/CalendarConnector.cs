using FrazzApps.Xamarin.CalendarConnector.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.CalendarConnector.Android;
using Android.Content;
using Android.Provider;
using Android.App;

[assembly: Dependency(typeof(CalendarConnector))]
namespace FrazzApps.Xamarin.CalendarConnector.Android
{
    /// <summary>
    /// CalendarConnector Implementation
    /// </summary>
    public class CalendarConnector : ICalendarConnector
    {

        private static Activity Activity;

        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init(Activity activity) { CalendarConnector.Activity = activity; }

        //android.permission.WRITE_CALENDAR


        public void AddAppointment(DateTime startTime, DateTime endTime, String subject, String location, String details, Boolean isAllDay, AppointmentReminder reminder, AppointmentStatus status)
        {
            ContentValues eventValues = new ContentValues();

            eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId, 1);
            //_calId);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Title, subject);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, details);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart, startTime.ToUniversalTime().ToString());
            // GetDateTimeMS(2011, 12, 15, 10, 0));
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend, endTime.ToUniversalTime().ToString());
            // GetDateTimeMS(2011, 12, 15, 11, 0));
            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, "UTC");
            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "UTC");
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Availability, ConvertAppointmentStatus(status));
            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventLocation, location);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.AllDay, (isAllDay) ? "1" : "0");
            eventValues.Put(CalendarContract.Events.InterfaceConsts.HasAlarm, "1");

            var eventUri = CalendarConnector.Activity.ContentResolver.Insert(CalendarContract.Events.ContentUri, eventValues);
            long eventID = long.Parse(eventUri.LastPathSegment);
            ContentValues remindervalues = new ContentValues();
            remindervalues.Put(CalendarContract.Reminders.InterfaceConsts.Minutes, ConvertReminder(reminder));
            remindervalues.Put(CalendarContract.Reminders.InterfaceConsts.EventId, eventID);
            remindervalues.Put(CalendarContract.Reminders.InterfaceConsts.Method, (int)RemindersMethod.Alert);
            var reminderURI = CalendarConnector.Activity.ContentResolver.Insert(CalendarContract.Reminders.ContentUri, remindervalues);

        }



        private int ConvertReminder(AppointmentReminder reminder)
        {
            switch (reminder)
            {
                case AppointmentReminder.none:
                    return 0; ///todo should this be null?
                case AppointmentReminder.five:
                    return 5;
                case AppointmentReminder.fifteen:
                    return 15;
                case AppointmentReminder.thirty:
                    return 30;
            }
            return 0;
        }


        private string ConvertAppointmentStatus(AppointmentStatus status)
        {
            switch (status)
            {
                case AppointmentStatus.busy:
                    return "Busy";
                case AppointmentStatus.free:
                    return "Free";
                case AppointmentStatus.tentative:
                    return "Tentative";
                case AppointmentStatus.outofoffice:
                    return "Unavailable";
            }
            return "";
        }
 
    }
}
