using SQLite;
namespace RSVPServ.Models;

public class Event
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Host { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public DateTime Deadline { get; set; }
    public int AttendeeLimit { get; set; }
    public int HostId { get; set; } // Capitalized for consistency

    [Ignore]
    public List<string> AttendeeList { get; set; } = new List<string>();

    [Ignore]
    public bool IsCurrentUserHost
    {
        get
        {
            // This 'if' block only exists for the MAUI App
#if APP_CLIENT
                if (App.IsGuest) return false;
                return HostId == App.CurrentUserId;
#else
            // The server doesn't care about "Current User" in the model itself
            return false;
#endif
        }
    }
}