using System;
using System.Linq;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace TimeApp
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<EventItem> Events { get; set; }

        public MainPage()
        {
            InitializeComponent();

            Events = new ObservableCollection<EventItem>
            {
                new EventItem { Title = "Mom's Birthday", TargetDate = new DateTime(DateTime.Now.Year, 10, 24) },
                new EventItem { Title = "Final Exam", TargetDate = DateTime.Now.AddDays(27) }
            };

            EventsList.ItemsSource = Events;
        }

        // 1. Click on the blank area of the card to jump to the detail page.
        private async void OnEventSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is EventItem selectedEvent)
            {
                await Navigation.PushAsync(new DetailPage(selectedEvent));
                ((CollectionView)sender).SelectedItem = null;
            }
        }

        // 2. Click the + button at the bottom right to navigate to the add page.
        private async void OnAddEventClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddEventPage(Events));
        }

        // 3. Click the red "×" button on the card to trigger the deletion.
        private async void OnExplicitDeleteClicked(object sender, EventArgs e)
        {
            var button = sender as Button;

            // Determine which event card the currently clicked button belongs to
            if (button?.BindingContext is EventItem eventToDelete)
            {
                bool confirm = await DisplayAlert("Delete Event",
                                                  $"Are you sure you want to delete '{eventToDelete.Title}'?",
                                                  "Yes", "No");

                if (confirm)
                {
                    Events.Remove(eventToDelete);
                }
            }
        }
    }
}
