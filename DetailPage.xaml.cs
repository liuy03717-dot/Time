namespace TimeApp;

public partial class DetailPage : ContentPage
{
    // 构造函数接收一个标题参数
    public DetailPage(string eventTitle)
    {
        InitializeComponent();

        // 设置页面数据
        this.Title = eventTitle;
        EventTitleLabel.Text = $"Days left until {eventTitle}";

       
        if (eventTitle.Contains("Exam"))
        {
            DaysLabel.Text = "27";
        }
    }
}