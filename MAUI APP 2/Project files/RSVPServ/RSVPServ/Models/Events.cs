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

    //storing names and emails of people who have RSVP'd to the event
    [Ignore]
    public List<string> AttendeeList { get; set; } = new List<string>();

    // Login helper
    public bool isUserCreator(string currentUserEmail) => Host == currentUserEmail;
}
