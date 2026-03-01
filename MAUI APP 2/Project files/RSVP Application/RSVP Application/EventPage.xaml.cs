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

        bool isCreator = _currentEvent.isUserCreator(App.CurrentUserName);

        CreatorSection.IsVisible = isCreator;
        GuestSection.IsVisible = !isCreator;

        EventNameEntry.IsReadOnly = !isCreator;
        EventLocationEntry.IsReadOnly = !isCreator;
        EventDescriptionEditor.IsReadOnly = !isCreator;
    }

    private async void OnRSVP_Clicked(object sender, EventArgs e)
    {
        await DisplayAlertAsync("Success", "You are on the list!", "OK");
        await Navigation.PopAsync();
    }

    private async void OnDecline_Clicked(object sender, EventArgs e) => await Navigation.PopAsync();

    private async void OnDeleteEvent_Clicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlertAsync("Delete", "Are you sure?", "Yes", "No");
        if (confirm) await Navigation.PopAsync();
    }

    private async void OnSaveChanges_Clicked(object sender, EventArgs e) => await Navigation.PopAsync();
}