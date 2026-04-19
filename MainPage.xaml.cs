using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;

namespace TimeApp
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<EventItem> Events { get; set; } = new ObservableCollection<EventItem>();

        // Instantiate the database assistant
        DatabaseService _dbService = new DatabaseService();

        public MainPage()
        {
            InitializeComponent();
            EventsList.ItemsSource = Events;
        }

        // efresh the data each time the page is displayed (or when returning from the background)
        protected override async void OnAppearing()
        {  
            base.OnAppearing();
            await LoadDataFromSqlite();
        }

        private async Task LoadDataFromSqlite()
        {
            // Retrieve all data from SQLite
            var items = await _dbService.GetEvents();

            Events.Clear();
            foreach (var item in items)
            {
                Events.Add(item);
            }
        }

        // Click on the card to jump to the details.
        private async void OnEventTapped(object sender, EventArgs e)
        {
            var layout = sender as BindableObject;
            if (layout?.BindingContext is EventItem selectedEvent)
            {
                try { HapticFeedback.Default.Perform(HapticFeedbackType.Click); } catch { }
                await Navigation.PushAsync(new DetailPage(selectedEvent));
            }
        }

        // Go to the add page
        private async void OnAddEventClicked(object sender, EventArgs e)
        {
            // After using SQLite, there is no longer a need to pass the Events collection.
            await Navigation.PushAsync(new AddEventPage());
        }

        // Click the X button to delete.
        private async void OnExplicitDeleteClicked(object sender, EventArgs e)
        {
            // Physical vibration feedback
            try { Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(50)); } catch { }

            var button = sender as Button;
            if (button?.BindingContext is EventItem eventToDelete)
            {
                bool confirm = await DisplayAlert("Delete", $"Delete '{eventToDelete.Title}'?", "Yes", "No");

                if (confirm)
                {
                    // 1. 从 SQLite 数据库删除Delete from the SQLite database
                    await _dbService.DeleteEvent(eventToDelete);
                    // 2. 从界面 UI 移除Remove from the interface UI
                    Events.Remove(eventToDelete);
                }
            }
        }
    }
}
