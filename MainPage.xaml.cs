using System.Collections.ObjectModel;

namespace TimeApp;

public partial class MainPage : ContentPage
{
    public ObservableCollection<EventItem> Events { get; set; }

    public MainPage()
    {
        InitializeComponent();

        // 模拟数据
        Events = new ObservableCollection<EventItem>
        {
            new EventItem { Title = "Mom's Birthday", Date = "Oct 24, 2026", DaysLeft = "5" },
            new EventItem { Title = "Final Exam", Date = "Nov 15, 2026", DaysLeft = "27" }
        };

        EventsList.ItemsSource = Events;
    }

    // 点击卡片跳转
    private async void OnEventTapped(object sender, EventArgs e)
    {
        var frame = sender as Frame;
        var selectedEvent = frame?.BindingContext as EventItem;

        if (selectedEvent != null)
        {
            // 将选中的标题传递给下一页
            await Navigation.PushAsync(new DetailPage(selectedEvent.Title));
        }
    }
}

public class EventItem
{
    public string Title { get; set; }
    public string Date { get; set; }
    public string DaysLeft { get; set; }
}