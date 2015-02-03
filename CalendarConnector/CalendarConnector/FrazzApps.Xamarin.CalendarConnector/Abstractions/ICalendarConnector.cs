using System;

namespace  FrazzApps.Xamarin.CalendarConnector.Abstractions
{
    /// <summary>
    /// CalendarConnector Interface
    /// </summary>
    public interface ICalendarConnector
    {
        void AddAppointment(DateTime startTime, DateTime endTime, String subject, String location, String details, Boolean isAllDay, AppointmentReminder reminder, AppointmentStatus status);
    }
}
