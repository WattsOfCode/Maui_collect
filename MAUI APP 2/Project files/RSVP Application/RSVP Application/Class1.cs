using SQLite;

namespace RSVP_Application;
public class DatabaseService
{
    private SQLiteConnection _database;
    public DatabaseService(string dbPath)
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "RSVPData.db3");
        _database = new SQLiteAsyncConnection(dbPath);

        _database.CreateTableAsync<User>().Wait();
        _database.CreateTableAsync<Event>().Wait();
    }

    // --- EVENT METHODS ---

    public async Task<List<Event>> GetEventsAsync()
    {
        return await _database.Table<Event>().ToListAsync();
    }

    public async Task SaveEventAsync(Event ev)
    {
        if (ev.Id != 0) {
            await _database.UpdateAsync(ev);
        } else {
            await _database.InsertAsync(ev);
        }
    }

    public async Task DeleteEventAsync(Event ev)
    {
        await _database.DeleteAsync(ev);
    }

    // --- USER METHODS (For Login/Register) ---

    public async Task<int> RegisterUserAsync(User user)
    {
        return await _database.InsertAsync(user);
    }

    public async Task<User> GetUserAsync(string username, string password)
    {
        return await _database.Table<User>()
            .Where(u => u.Username == username && u.Password == password)
            .FirstOrDefaultAsync();
    }
    } 
}
