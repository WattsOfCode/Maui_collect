using SQLite;

namespace Data_Transfering_SQLite.Model
{
    public class Item
    {
        [PrimaryKey] 
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
    }
}
