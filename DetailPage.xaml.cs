using System;
using Microsoft.Maui.Controls;

namespace TimeApp
{
    public partial class DetailPage : ContentPage
    {
        public DetailPage(EventItem selectedEvent)
        {
            InitializeComponent();

            this.Title = selectedEvent.Title;

            // Dynamically determine whether it is the past, present or future, and modify the copy and color.
            if (selectedEvent.DaysLeft < 0)
            {
                DaysLabel.Text = Math.Abs(selectedEvent.DaysLeft).ToString();
                DaysLabel.TextColor = Colors.Red;
                EventTitleLabel.Text = $"Days passed since {selectedEvent.Title}";
            }
            else if (selectedEvent.DaysLeft == 0)
            {
                DaysLabel.Text = "0";
                DaysLabel.TextColor = Colors.Green;
                EventTitleLabel.Text = $"Today is {selectedEvent.Title}!";
            }
            else
            {
                DaysLabel.Text = selectedEvent.DaysLeft.ToString();
                DaysLabel.TextColor = Color.FromArgb("#3F51B5");
                EventTitleLabel.Text = $"Days left until {selectedEvent.Title}";
            }
        }
    }
}
