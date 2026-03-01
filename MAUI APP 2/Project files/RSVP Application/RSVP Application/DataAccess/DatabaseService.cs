using SQLite;
using RSVP_Application.Models;

public class DatabaseService
{
    private SQLiteAsyncConnection _database;

    async Task Init()
    {
        if (_database is not null) return;

        // Creates the local DB file in the app's sandboxed folder
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "EventsDB.db3");
        _database = new SQLiteAsyncConnection(dbPath);
        await _database.CreateTableAsync<Event>();
    }

    public async Task<List<Event>> GetEventsAsync()
    {
        await Init();
        return await _database.Table<Event>().ToListAsync();
    }

    public async Task SaveEventAsync(Event ev)
    {
        await Init();
        await _database.InsertAsync(ev);
    }
}