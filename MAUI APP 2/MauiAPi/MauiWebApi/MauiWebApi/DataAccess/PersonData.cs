
using MauiWebApi.Models;
using SQLite;

namespace MauiWebApi.DataAccess
{
    public class PersonData
    {
        SQLiteConnection database;

        public void Init()
        {
            if (database is not null) { return; }
            database = new SQLiteConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags);
            database.CreateTable<Person>();
        }
        public List<Person> GetPeople()
        {
            Init();
            return database.Table<Person>().ToList();
        }
        public int SavePerson(Person person)
        {
            Init();
            if (person.Id != 0) {
                // Update an existing person
                return database.Update(person); }
            else
            {
                // Save a new person
                return database.Insert(person);
            }
        }
    }
}
