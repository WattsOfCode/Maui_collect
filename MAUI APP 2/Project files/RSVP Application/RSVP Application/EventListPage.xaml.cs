namespace RSVP_Application;

public partial class EventListPage : ContentPage
{
	async void OnEventSelected(object sender, SelectionChangedEventArgs e)
	{
        if (e.CurrentSelection.FirstOrDefault() is Event selectedEvent)
        {
            await Navigation.PushAsync(new EventPage(selectedEvent));
        }
    ((CollectionView)sender).SelectedItem = null;
    }

    //Navigating to the Create Event flow
    async void OnCreateEvent_Clicked(object sender, EventArgs e)
    {
        // Navigate to the creation page 
        await Navigation.PushAsync(new CreateEventPage());
    
    }
}