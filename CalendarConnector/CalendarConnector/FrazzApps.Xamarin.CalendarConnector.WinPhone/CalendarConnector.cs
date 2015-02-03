using  FrazzApps.Xamarin.CalendarConnector.Abstractions;
using System;
using Xamarin.Forms;
using  FrazzApps.Xamarin.CalendarConnector.WindowsPhone;
using Microsoft.Phone.Tasks;

[assembly: Dependency(typeof(CalendarConnector))]
namespace  FrazzApps.Xamarin.CalendarConnector.WindowsPhone
{
    /// <summary>
    /// CalendarConnector Implementation
    /// </summary>
    public class CalendarConnector : ICalendarConnector
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { }


        public void AddAppointment(DateTime startTime, DateTime endTime, String subject, String location, String details, Boolean isAllDay, AppointmentReminder reminder, AppointmentStatus status)
        {
            SaveAppointmentTask saveAppointmentTask = new SaveAppointmentTask();

            saveAppointmentTask.StartTime = startTime;
            saveAppointmentTask.EndTime = endTime;
            saveAppointmentTask.Subject = subject;
            saveAppointmentTask.Location = location;
            saveAppointmentTask.Details = details;
            saveAppointmentTask.IsAllDayEvent = isAllDay;
            saveAppointmentTask.Reminder = ConvertReminder(reminder);
            saveAppointmentTask.AppointmentStatus = ConvertAppointmentStatus(status);

            saveAppointmentTask.Show();
        }

        private Microsoft.Phone.Tasks.Reminder ConvertReminder(AppointmentReminder reminder)
        {
            switch(reminder)
            {
                case AppointmentReminder.none:
                    return Reminder.None;
                case AppointmentReminder.five:
                    return Reminder.FiveMinutes;
                case AppointmentReminder.fifteen:
                    return Reminder.FifteenMinutes;
                case AppointmentReminder.thirty:
                    return Reminder.ThirtyMinutes;
            }
            return Reminder.AtStartTime;
        }


        private Microsoft.Phone.UserData.AppointmentStatus ConvertAppointmentStatus(AppointmentStatus status)
        {
            switch (status)
            {
                case AppointmentStatus.busy:
                    return Microsoft.Phone.UserData.AppointmentStatus.Busy;
                case AppointmentStatus.free:
                    return Microsoft.Phone.UserData.AppointmentStatus.Free;
                case AppointmentStatus.tentative:
                    return Microsoft.Phone.UserData.AppointmentStatus.Tentative;
                case AppointmentStatus.outofoffice:
                    return Microsoft.Phone.UserData.AppointmentStatus.OutOfOffice;
            }
            return Microsoft.Phone.UserData.AppointmentStatus.Busy;
        }
    }
}
