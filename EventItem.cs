using SQLite;

namespace TimeApp
{
    public class EventItem
    {
        [PrimaryKey, AutoIncrement] // Tell the database that this is the unique ID and it will be automatically incremented.
        public int Id { get; set; }

        public string Title { get; set; }
        public DateTime TargetDate { get; set; }

        [Ignore] // These two items are calculated and do not need to be stored in a database table.
        public int DaysLeft => (TargetDate.Date - DateTime.Today).Days;
        [Ignore]
        public string DateDisplay => TargetDate.ToString("MMM dd, yyyy");
    }
}
