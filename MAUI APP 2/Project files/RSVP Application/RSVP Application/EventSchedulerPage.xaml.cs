using System.Net;
using RSVP_Application.Models;

namespace RSVP_Application;

public partial class EventSchedulerPage : ContentPage
{
    public EventSchedulerPage()
    {
        InitializeComponent();
    }

    private async void OnSaveEventClicked(object sender, EventArgs e)
    {
        // REQUIREMENT: Validate that all fields have data entered
        if (string.IsNullOrWhiteSpace(NameEntry.Text) ||
            string.IsNullOrWhiteSpace(AddressEntry.Text) ||
            string.IsNullOrWhiteSpace(DescriptionEditor.Text))
        {
            await DisplayAlertAsync("Incomplete Form", "Please fill in all fields before saving.", "OK");
            return;
        }

        // REQUIREMENT: Saving not functional this week
        await DisplayAlertAsync("Success", "Event scheduled (Simulated)", "OK");
        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        // REQUIREMENT: Navigate away using Cancel button
        await Navigation.PopAsync();
    }
}