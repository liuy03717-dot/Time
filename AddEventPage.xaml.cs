using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace TimeApp
{
    public partial class AddEventPage : ContentPage
    {
        private ObservableCollection<EventItem> _mainEvents;

        public AddEventPage(ObservableCollection<EventItem> events)
        {
            InitializeComponent();
            _mainEvents = events;
            DatePicker.Date = DateTime.Today;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TitleEntry.Text))
            {
                // Use ?? DateTime.Today to solve the nullable date error in the latest version of MAUI.
                var newEvent = new EventItem
                {
                    Title = TitleEntry.Text,
                    TargetDate = DatePicker.Date ?? DateTime.Today
                };

                _mainEvents.Add(newEvent);
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Please enter an event name.", "OK");
            }
        }
    }
}