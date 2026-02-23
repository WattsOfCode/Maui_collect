using SQLite;
using Data_Transfering_SQLite.Model;

namespace Data_Transfering_SQLite.Model
{
    public static class DatabaseConstants
    {
        public const string DatabaseFileName = "items.db";

        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);
    }
}