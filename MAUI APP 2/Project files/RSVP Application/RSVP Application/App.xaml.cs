using Microsoft.Extensions.DependencyInjection;

namespace RSVP_Application;

public partial class App : Application
{
    // tracking variables for user state
    public static string CurrentUserName { get; set; }
    public static string CurrentUserEmail { get; set; }
    public static bool IsGuest { get; set; }

    public App()
    {
        InitializeComponent();
        
        MainPage = new NavigationPage(new MainPage()); 
        Database = new DatabaseService();
    }

    public static DatabaseService Database { get; private set; }
}