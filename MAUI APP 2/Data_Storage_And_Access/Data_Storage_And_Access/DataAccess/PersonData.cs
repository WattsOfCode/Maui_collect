
using Data_Storage_And_Access.Models;
using SQLite;

namespace Data_Storage_And_Access.DataAccess
{
    public class PersonData
    {
        SQLiteAsyncConnection database;
        async Task Init()
        {
            if (database is not null) { return; }
            database = new SQLiteAsyncConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags);
            await database.CreateTableAsync<Person>();
        }
        public async Task<List<Person>> GetPeopleAsync() {
            await Init();
            return await database.Table<Person>().ToListAsync();
        }
        public async Task<Person> GetPersonAsync(int id) {
            await Init();
            return await database.Table<Person>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }
        public async Task<int> SavePersonAsync(Person person) {
            await Init();
            if (person.Id != 0) {
                return await database.UpdateAsync(person);
            } else {
                return await database.InsertAsync(person);
            }
        }
        public async Task<int> DeletePersonAsync(Person person) {
            await Init();
            return await database.DeleteAsync(person);
        }
        public async Task ClearAllPeropleAsync() 
        { 
            await Init(); 
            await database.DeleteAllAsync<Person>(); 
        }
    }
}
