using RSVP_Application.Models;
namespace RSVP_Application;

public partial class EventListPage : ContentPage
{
    public List<Event> allEvents { get; set; } = new List<Event>();

    public EventListPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        CreateEventButton.IsVisible = !App.IsGuest;
        
        await LoadEventsFromDatabase();
        
        EventListView.ItemsSource = allEvents;
    }

    private async Task LoadEventsFromDatabase()
    {
        //display listing of events that were stored
        allEvents = await App.Database.GetEventsAsync();
        EventListView.ItemsSource = allEvents;
    }

    private void OnFilterChanged(object sender, EventArgs e)
    {
        if (FilterPicker.SelectedItem == null) return;

        var selected = FilterPicker.SelectedItem.ToString();

        if (selected == "Events I'm Hosting")
            {
                EventListView.ItemsSource = allEvents.Where(x => x.Host == App.CurrentUserName).ToList();
            }
            else
            {
                EventListView.ItemsSource = allEvents;
            }
    } 
    
    private async void OnEventSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Event selectedEvent)
        {
            await Navigation.PushAsync(new EventPage(selectedEvent));
            ((CollectionView)sender).SelectedItem = null;
        }
    }

    private async void OnCreateEvent_Clicked(object sender, EventArgs e)
    { 
        if (App.IsGuest) {
            await DisplayAlertAsync("Access Denied", "You must be logged in to create an event.", "OK");
            return;
        }

        await Navigation.PushAsync(new EventSchedulerPage());
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlertAsync("Logout", "Are you sure you want to log out?", "Yes", "No");
        if (confirm)
        {
            App.IsGuest = true;
            App.CurrentUserName = string.Empty;
            Application.Current.MainPage = new NavigationPage(new MainPage());
            
        }
    }   
}