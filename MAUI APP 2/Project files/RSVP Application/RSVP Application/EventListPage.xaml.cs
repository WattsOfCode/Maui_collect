using RSVP_Application.Models;
namespace RSVP_Application;

public partial class EventListPage : ContentPage
{
    public List<Event> HardCodedEvents { get; set; }

    public EventListPage()
    {
        InitializeComponent();

        //dumby data for testing - in a real app this would come from a database or API
        HardCodedEvents = new List<Event>
        {
            new Event { Name = "Tech Workshop", Host = "Admin User", Address = "Room 101", Date = DateTime.Now.AddDays(2) },
            new Event { Name = "Community BBQ", Host = "John Doe", Address = "North Park", Date = DateTime.Now.AddDays(5) },
            new Event { Name = "Project Deadline", Host = "Admin User", Address = "Online", Date = DateTime.Now.AddDays(1) }
        };

        EventListView.ItemsSource = HardCodedEvents;
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
    { await Navigation.PushAsync(new EventSchedulerPage());}
    private void OnFilterChanged(object sender, EventArgs e)
    {
        var selected = FilterPicker.SelectedItem.ToString();

        if (selected == "Events I'm Hosting") {
            EventListView.ItemsSource = HardCodedEvents.Where(x => x.Host == App.CurrentUserName).ToList();
        } else {
            EventListView.ItemsSource = HardCodedEvents;
        }
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        // REQUIREMENT: Log out of the app
        bool confirm = await DisplayAlertAsync("Logout", "Are you sure you want to log out?", "Yes", "No");
        if (confirm)
        {
            // Resets the app to the Login Screen (MainPage)
            if (App.Current?.Windows.Count > 0)
            {
                App.Current.Windows[0].Page = new NavigationPage(new MainPage());
            }
        }
    }   
}