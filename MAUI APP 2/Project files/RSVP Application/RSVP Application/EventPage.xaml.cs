using RSVP_Application.Models;

namespace RSVP_Application;

public partial class EventPage : ContentPage
{
    private Event _currentEvent;

    public EventPage(Event selectedEvent)
    {
        InitializeComponent();
        _currentEvent = selectedEvent;

        EventNameEntry.Text = _currentEvent.Name;
        EventDatePicker.Date = _currentEvent.Date;
        EventTimePicker.Time = _currentEvent.Date.TimeOfDay;
        EventLocationEntry.Text = _currentEvent.Address;
        EventDescriptionEditor.Text = _currentEvent.Description;

        AttendeesListView.ItemsSource = _currentEvent.AttendeeList;

        bool isCreator = false;
        if (!App.IsGuest)
        {
            isCreator = (_currentEvent.hostId == App.CurrentUserId);
        }

        if (App.IsGuest) {
            GuestSection.IsVisible = false;
            CreatorSection.IsVisible = false;
        } else {
            CreatorSection.IsVisible = isCreator;
            GuestSection.IsVisible = !isCreator;
        }
                
        EventNameEntry.IsReadOnly = !isCreator;
        EventLocationEntry.IsReadOnly = !isCreator;
        EventDescriptionEditor.IsReadOnly = !isCreator;
    }

    private async void OnRSVP_Clicked(object sender, EventArgs e)
    {
        if (_currentEvent.AttendeeList.Contains(App.CurrentUserName))
        {
            await DisplayAlert("Notice", "You are already on the list!", "OK");
            return;
        }

        _currentEvent.AttendeeList.Add(App.CurrentUserName);

        AttendeesListView.ItemsSource = null;
        AttendeesListView.ItemsSource = _currentEvent.AttendeeList;

        var apiService = new DataAccess.WebService();
        bool success = await apiService.PostRSVPToServer(_currentEvent.Id, App.CurrentUserName);
        await DisplayAlert("Success", "You've been added to the guest list!", "OK");
    }

    private async void OnDecline_Clicked(object sender, EventArgs e) => await Navigation.PopAsync();

    private async void OnDeleteEvent_Clicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Delete", "Are you sure you want to delete this event?", "Yes", "No");
        if (confirm)
        {
            // Add your deletion logic here (Local DB + Server)
            await Navigation.PopAsync();
        }
    }

    private async void OnSaveChanges_Clicked(object sender, EventArgs e)
    {
        // Update the object from the entries
        _currentEvent.Name = EventNameEntry.Text;
        _currentEvent.Address = EventLocationEntry.Text;
        _currentEvent.Date = EventDatePicker.Date.GetValueOrDefault() + EventTimePicker.Time.GetValueOrDefault();

        // Save locally and to server
        await App.Database.SaveEventAsync(_currentEvent);

        await DisplayAlert("Success", "Changes saved!", "OK");
        await Navigation.PopAsync();
    }
}