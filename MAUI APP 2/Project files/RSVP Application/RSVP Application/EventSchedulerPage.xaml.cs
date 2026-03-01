using RSVP_Application.Models;

namespace RSVP_Application;

public partial class EventSchedulerPage : ContentPage
{
    public EventSchedulerPage()
    {
        InitializeComponent();
        if (App.IsGuest)
        {
            HostLabel.Text = "Host: Guest";
            NameEntry.IsEnabled = false;
            AddressEntry.IsEnabled = false;
            DescriptionEditor.IsEnabled = false;
            LimitEntry.IsEnabled = false;
            saveButton.IsVisible = false;
        }
        else
        {
            HostLabel.Text = $"Host: {App.CurrentUserName}";
        }
    }

    private async void OnSaveEventClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text))
        {
            await DisplayAlert("Error", "Please enter an event name.", "OK");
            return;
        }

        var newEvent = new Event
        {
            Name = NameEntry.Text,
            Address = AddressEntry.Text,
            Description = DescriptionEditor.Text,
            hostId = App.CurrentUserId,

            Date = (DatePicker.Date ?? DateTime.Now).Add(TimePicker.Time ?? TimeSpan.Zero),
            Deadline = (DeadlineDatePicker.Date ?? DateTime.Now).Add(DeadlineTimePicker.Time ?? TimeSpan.Zero),

            AttendeeLimit = int.TryParse(LimitEntry.Text, out int limit) ? limit : 0,
            Host = App.IsGuest ? "Guest" : App.CurrentUserName
        };

        try
        {
            await App.Database.SaveEventAsync(newEvent);

            var apiService = new DataAccess.WebService();
            bool serverSuccess = await apiService.PostEventToServer(newEvent);

            if (!serverSuccess)
            {
                await DisplayAlert("Warning", "Event saved locally but failed to sync with server.", "OK");
            }
            else
            {
                await DisplayAlert("Success", "Event saved and synced with server!", "OK");
            }

            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to save event: {ex.Message}", "OK");
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}