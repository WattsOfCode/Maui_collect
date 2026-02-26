using MAUIREST.DataAccess; // Ensure this points to your ItemService
using MAUIREST.Models;

namespace MAUIREST;

public partial class DataPage : ContentPage
{
    string _user = "Deardorff01"
        , _pass = "Password1";
    // Make sure you have an ItemService class that handles the API calls
    DataAccess.ItemService _itemService = new DataAccess.ItemService();
    public DataPage()
    {
        InitializeComponent();
        LoadData();
    }

    private async void LoadData()
    {
        var items = await _itemService.GetItemsAsync(_user, _pass);
        ItemsListView.ItemsSource = items;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var newItem = new Models.Item
        {
            ItemName = ItemNameEntry.Text,
            ItemDescription = ItemDescriptionEntry.Text
        };

        bool saved = await _itemService.SaveItemAsync(newItem, _user, _pass);

        if (saved)
        {
            ItemNameEntry.Text = string.Empty;
            ItemDescriptionEntry.Text = string.Empty;
            LoadData(); // Refresh the list from the database
        }
    }
}