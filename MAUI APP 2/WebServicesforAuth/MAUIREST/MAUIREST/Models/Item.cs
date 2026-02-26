using SQLite;
namespace MAUIREST.Models
{
    public class Item

    {
        [PrimaryKey, AutoIncrement]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
    }
}
