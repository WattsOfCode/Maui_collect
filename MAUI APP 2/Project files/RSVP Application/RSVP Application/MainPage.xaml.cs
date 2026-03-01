using RSVP_Application.Models;
namespace RSVP_Application;

public partial class MainPage : ContentPage
{
    public MainPage() => InitializeComponent();

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        // Requirement: Validate as a hard-coded user
        if (Username.Text == "admin" && Password.Text == "password")
        {
            App.IsGuest = false;
            App.CurrentUserName = "Admin User";
            await Navigation.PushAsync(new EventListPage());
        }
        else
        {
            // Requirement: Log in as a guest
            // FIX: Changed DisplayAlertAsync to DisplayAlert
            bool answer = await DisplayAlertAsync("Login Failed", "Continue as Guest?", "Yes", "No");
            if (answer)
            {
                App.IsGuest = true;
                App.CurrentUserName = "Guest";
                await Navigation.PushAsync(new EventListPage());
            }
        }
    }

    private async void OnRegister_Clicked(object sender, EventArgs e)
    {
        // Requirement: Navigate to and away from the Add User screen
        await Navigation.PushAsync(new SignUpPage());
    }

    // FIX: Added missing method referenced in XAML
    private async void OnForgotLogin_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Help", "Please contact the administrator to reset your password.", "OK");
    }
}