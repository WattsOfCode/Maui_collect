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
        await _database.CreateTableAsync<User>();
    }
    // USER METHODS
    public async Task RegisterUserAsync(User user)
    {
        await Init();
        await _database.InsertAsync(user);
    }

    public async Task<bool> IsUsernameTakenAsync(string username)
    {
        await Init();
        var existingUser = await _database.Table<User>()
                                     .Where(u => u.Username == username)
                                     .FirstOrDefaultAsync();
        return existingUser != null;
    }

    public async Task<User> GetUserAsync(string username, string password)
    {
        await Init();
        return await _database.Table<User>()
            .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
    }

    //EVENT METHODS
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