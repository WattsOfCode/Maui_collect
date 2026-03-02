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
        if (_currentEvent.AttendeeList.Count >= _currentEvent.AttendeeLimit)
        {
            await DisplayAlert("Full", "Sorry, this event has reached its attendee limit.", "OK");
            return;
        }

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
        bool confirm = await DisplayAlert("Delete Event", "Are you sure you want to delete this event?", "Yes", "No");

        if (confirm)
        {

            int rowsAffected = await App.Database.DeleteEventAsync(_currentEvent);
            if (rowsAffected > 0)
            {
                //Sync with WebService if needed
                var apiService = new DataAccess.WebService();
                
                await apiService.DeleteEventFromServer(_currentEvent.Id);
                await DisplayAlert("Deleted", "The event has been removed.", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Could not delete the event from the database.", "OK");
            }
        }
    }

    private async void OnSaveChanges_Clicked(object sender, EventArgs e)
    {
        _currentEvent.Name = EventNameEntry.Text;
        _currentEvent.Address = EventLocationEntry.Text;
        _currentEvent.Date = EventDatePicker.Date.GetValueOrDefault() + EventTimePicker.Time.GetValueOrDefault();

        await App.Database.SaveEventAsync(_currentEvent);

        await DisplayAlert("Success", "Changes saved!", "OK");
        await Navigation.PopAsync();
    }
}