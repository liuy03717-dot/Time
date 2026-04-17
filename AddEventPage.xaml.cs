using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;

namespace TimeApp
{
    public partial class AddEventPage : ContentPage
    {
        // Service for SQLite database operations
        private readonly DatabaseService _dbService = new DatabaseService();

        public AddEventPage()
        {
            InitializeComponent();

            // Set the default date to today when the page opens
            DatePicker.Date = DateTime.Today;
        }

        /// <summary>
        /// Handles the Save button click event.
        /// Performs data validation, persistence, and schedules a notification.
        /// </summary>
        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // 1. Validation: Ensure the event title is not empty
            if (!string.IsNullOrWhiteSpace(TitleEntry.Text))
            {
                // Provide a long-press haptic feedback for a successful "save" action
                try { HapticFeedback.Default.Perform(HapticFeedbackType.LongPress); } catch { }

                // 2. Data Preparation
                // Handling Nullable DateTime? using null-coalescing operator to avoid CS0266 error
                DateTime selectedDate = DatePicker.Date ?? DateTime.Today;

                var newEvent = new EventItem
                {
                    Title = TitleEntry.Text,
                    TargetDate = selectedDate
                };

                // 3. Persistence: Save the record into the local SQLite database
                await _dbService.AddEvent(newEvent);

                // 4. Advanced Feature: Schedule a local notification
                // Only schedule if the date is in the future or today
                if (selectedDate.Date >= DateTime.Today)
                {
                    // Calls the custom NotificationService to register a system alarm
                    await NotificationService.ScheduleNotification(newEvent.Title, selectedDate);
                }

                // 5. Navigation: Return to the previous page (MainPage)
                // MainPage will refresh its list in the OnAppearing method
                await Navigation.PopAsync();
            }
            else
            {
                // Show a warning if the user tries to save an empty title
                await DisplayAlert("Requirement", "Please enter a title for your event.", "OK");
            }
        }
    }
}
