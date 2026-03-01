using RSVP_Application.Models;
namespace RSVP_Application;

public partial class MainPage : ContentPage
{
    public MainPage() => InitializeComponent();


    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        // validation inputs
        if (string.IsNullOrWhiteSpace(Username.Text) || string.IsNullOrWhiteSpace(Password.Text))
        {
            await DisplayAlertAsync("Error", "Please enter credentials", "OK");
            return;
        }
        // database check
        var user = await App.Database.GetUserAsync(Username.Text, Password.Text);
        if (user != null)
        {
            App.IsGuest = false; 
            App.CurrentUserId = user.Id;
            App.CurrentUserName = user.FullName;
            App.CurrentUserEmail = user.Email; 
            await Navigation.PushAsync(new EventListPage());
        }
        else
        {
            //fall back for guest access
            bool answer = await DisplayAlertAsync("Login Failed", "User not found. Continue as Guest?", "Yes", "No");
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
        await Navigation.PushAsync(new SignUpPage());
    }

    private async void OnForgotLogin_Clicked(object sender, EventArgs e)
    {
        await DisplayAlertAsync("Help", "Please contact the administrator to reset your password.", "OK");
    }
}