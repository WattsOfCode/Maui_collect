using MAUIREST.Models;
using SQLite;

namespace MAUIREST.DataAccess
{ 
    public class ItemData
    {
        SQLiteConnection database;

        public void Init()
        {
            if (database is not null) { return; }
            database = new SQLiteConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags);
            database.CreateTable<Item>();
        }
        public List<Item> GetItem()
        {
            Init();
            return database.Table<Item>().ToList();
        }
        public int SaveItem(Item item)
        {
            Init();
            if (item.ItemId != 0)
            {
                // Update an existing person
                return database.Update(item);
            }
            else
            {
                // Save a new person
                return database.Insert(item);
            }
        }
    }
}