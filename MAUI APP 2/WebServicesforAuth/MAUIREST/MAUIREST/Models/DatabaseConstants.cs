namespace MAUIREST.Models;
internal class DatabaseConstants
{
    public const string DatabaseFilename = "Item.db3";
    public const SQLite.SQLiteOpenFlags Flags =
        SQLite.SQLiteOpenFlags.ReadWrite
    |   SQLite.SQLiteOpenFlags.Create
    |   SQLite.SQLiteOpenFlags.SharedCache;
    public static string DatabasePath =>
        Path.Combine(AppContext.BaseDirectory, DatabaseFilename);
}