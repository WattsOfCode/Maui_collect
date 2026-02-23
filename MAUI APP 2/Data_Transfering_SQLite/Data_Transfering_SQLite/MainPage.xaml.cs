using Data_Transfering_SQLite.Model;
using Data_Transfering_SQLite.DataAccess;

namespace Data_Transfering_SQLite
{
    public partial class MainPage : ContentPage
    {
        ItemData _dbService = new ItemData();
        public MainPage() {
            InitializeComponent();
            _ = LoadItems();
        }

        private async void OnSaveClicked(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(txtItemID.Text) ||
                string.IsNullOrWhiteSpace(txtItemName.Text) ||
                string.IsNullOrWhiteSpace(txtItemDescription.Text))
            {
                await DisplayAlertAsync("Missing Data", "Please fill in all fields before saving.", "OK");
                return;
            }
            if (!int.TryParse(txtItemID.Text, out _))
            {
                await DisplayAlertAsync("Invalid ID", "The Item ID must be a number.", "OK");
                return;
            }

            var newItem = new Item
            {
                ItemID = txtItemID.Text,
                ItemName = txtItemName.Text,
                ItemDescription = txtItemDescription.Text
            };

            await _dbService.SaveItemAsync(newItem);

            txtItemID.Text = txtItemName.Text = txtItemDescription.Text = string.Empty;
            await LoadItems();
        }

        private async Task LoadItems() {
            var items = await _dbService.GetItemsAsync();
            lstItems.ItemsSource = items;
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItemID.Text))
            {
                await DisplayAlertAsync("Error", "Please enter the Item ID you wish to delete.", "OK");
                return;
            }
            bool confirm = await DisplayAlertAsync("Confirm", $"Delete Item ID {txtItemID.Text}?", "Yes", "No");
            if (!confirm) return;

            var items = await _dbService.GetItemsAsync();
            var itemsToDelete = new List<Item>();
            var button = (Button)sender;
            var itemToDelete = items.FirstOrDefault(i => i.ItemID == txtItemID.Text);

            if (itemToDelete != null)
            {
                await _dbService.DeleteItemAsync(itemToDelete);

                txtItemID.Text = string.Empty;
                await LoadItems();
                await DisplayAlertAsync("Success", "Item deleted.", "OK");
            } else
            {
                await DisplayAlertAsync("Not found", "No item exists with that ID.", "OK");
            }                    
        }
    }
}
