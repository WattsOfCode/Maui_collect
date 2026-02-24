using SQLite;

namespace MauiWebApi.Models
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public DateTime DoB { get; set; }
    }
}