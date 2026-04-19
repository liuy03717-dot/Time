using SQLite;

namespace TimeApp
{
    public class DatabaseService
    {
        SQLiteAsyncConnection _db;

        async Task Init()
        {
            if (_db != null) return;
            // The database files are stored in the local folder of the mobile phone.
            var path = Path.Combine(FileSystem.AppDataDirectory, "MyData.db3");
            _db = new SQLiteAsyncConnection(path);
            await _db.CreateTableAsync<EventItem>(); // If the table does not exist, create one.
        }

        public async Task<List<EventItem>> GetEvents()
        {
            await Init();
            return await _db.Table<EventItem>().ToListAsync();
        }

        public async Task AddEvent(EventItem item)
        {
            await Init();
            await _db.InsertAsync(item);
        }

        public async Task DeleteEvent(EventItem item)
        {
            await Init();
            await _db.DeleteAsync(item);
        }
    }
}