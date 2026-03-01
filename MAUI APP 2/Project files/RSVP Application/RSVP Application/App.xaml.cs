using Microsoft.Extensions.DependencyInjection;

namespace RSVP_Application;

public partial class App : Application
{
    // tracking variables for user state
    public static bool IsGuest { get; set; }
    public static string CurrentUserName { get; set; }

    public App()
    {
        InitializeComponent();

        // Wrap your MainPage (Login) in a NavigationPage 
        // This is what creates the "Back Button" automatically!
        MainPage = new NavigationPage(new MainPage());
    }
}