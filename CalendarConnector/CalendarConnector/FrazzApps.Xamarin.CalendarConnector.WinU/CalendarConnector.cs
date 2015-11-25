using FrazzApps.Xamarin.CalendarConnector.Abstractions;
using System;
using Xamarin.Forms;
using FrazzApps.Xamarin.CalendarConnector.WinU;
using Windows.ApplicationModel.Appointments;

[assembly: Dependency(typeof(CalendarConnector))]
namespace  FrazzApps.Xamarin.CalendarConnector.WinU
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
            var appointment = new Appointment();
            
            appointment.StartTime = startTime;
            appointment.Duration = endTime.Subtract(startTime);
            appointment.Subject = subject;
            appointment.Location = location;
            appointment.Details = details;
            appointment.AllDay = isAllDay;
            appointment.Reminder = ConvertReminder(reminder);
            appointment.BusyStatus = ConvertAppointmentStatus(status);
            
            AppointmentManager.ShowEditNewAppointmentAsync(appointment);
        }

        private TimeSpan? ConvertReminder(AppointmentReminder reminder)
        {
            switch(reminder)
            {
                case AppointmentReminder.none:
                    return null;
                case AppointmentReminder.five:
                    return new TimeSpan(0, 5, 0);
                case AppointmentReminder.fifteen:
                    return new TimeSpan(0, 15, 0);
                case AppointmentReminder.thirty:
                    return new TimeSpan(0, 30, 0);
            }
            return new TimeSpan(0, 0, 0);
        }


        private AppointmentBusyStatus ConvertAppointmentStatus(AppointmentStatus status)
        {
            switch (status)
            {
                case AppointmentStatus.busy:
                    return AppointmentBusyStatus.Busy;
                case AppointmentStatus.free:
                    return AppointmentBusyStatus.Free;
                case AppointmentStatus.tentative:
                    return AppointmentBusyStatus.Tentative;
                case AppointmentStatus.outofoffice:
                    return AppointmentBusyStatus.OutOfOffice;
            }
            return AppointmentBusyStatus.Busy;
        }
    }
}
