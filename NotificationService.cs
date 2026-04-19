using Plugin.LocalNotification;
using Plugin.LocalNotification.Core.Models;

namespace TimeApp
{
    public static class NotificationService
    {
        public static async Task ScheduleNotification(string title, DateTime targetDate)
        {
            // Check and request permissions
            if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
            {
                await LocalNotificationCenter.Current.RequestNotificationPermission();
            }

            // Create notification content
            var request = new NotificationRequest
            {
                NotificationId = new Random().Next(1000, 9999), // Unique ID
                Title = "Important Date Reminder⏰",
                Description = $"Today is：{title}！Remember to check it!！",
                BadgeNumber = 1,
                Schedule = new NotificationRequestSchedule
                {
                    // Set the reminder time. For example: 9:00 a.m. on the target date.
                    NotifyTime = DateTime.Now.AddSeconds(5),
                    // If the date has passed, then it will not be sent.
                }
            };

            await LocalNotificationCenter.Current.Show(request);
        }
    }
}