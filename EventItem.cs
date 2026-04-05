public class EventItem
{
    public string Title { get; set; }
    public DateTime TargetDate { get; set; }

    // Automatically calculate the remaining days
    public int DaysLeft => (TargetDate - DateTime.Today).Days;

    // Formatted date display, for example, "Oct 24, 2026"
    public string DateDisplay => TargetDate.ToString("MMM dd, yyyy");
}