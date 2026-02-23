using SQLite;
using Data_Transfering_SQLite.Model;

namespace Data_Transfering_SQLite.DataAccess
{
    public class ItemData
    {
        SQLiteAsyncConnection database;
        async Task Init()
        {
            if (database is not null) return;

            database = new SQLiteAsyncConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags);
            await database.CreateTableAsync<Item>();
        }
        public async Task<List<Item>> GetItemsAsync()
        {
            await Init();
            return await database.Table<Item>().ToListAsync();
        }
        public async Task<int> SaveItemAsync(Item item)
        {
            await Init();
            return await database.InsertOrReplaceAsync(item);
        }

        public async Task<int> DeleteItemAsync(Item item)
        {
            await Init();
            return await database.DeleteAsync(item);
        }
    }
}