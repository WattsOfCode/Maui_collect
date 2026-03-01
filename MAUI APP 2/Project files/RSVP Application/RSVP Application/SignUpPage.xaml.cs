using RSVP_Application.Models; 
using RSVP_Application.DataAccess;
namespace RSVP_Application;

public partial class SignUpPage : ContentPage
{
    public SignUpPage()
    {
        InitializeComponent();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        string typedUsername = NewUserName.Text;
        bool isTaken = await App.Database.IsUsernameTakenAsync(typedUsername);
        if (isTaken) {
            await DisplayAlertAsync("Username Taken", "The username is already taken. Please choose another one.", "OK");
            return;
        }

        // Validate all fields have data
        if (string.IsNullOrWhiteSpace(NewName.Text) ||
            string.IsNullOrWhiteSpace(NewUserName.Text) ||
            string.IsNullOrWhiteSpace(NewEmail.Text) ||
            string.IsNullOrWhiteSpace(NewPassword.Text) ||
            string.IsNullOrWhiteSpace(ConfirmPassword.Text))
        {
            await DisplayAlertAsync("Missing Info", "All fields are required.", "OK");
            return;
        }

        if (NewPassword.Text != ConfirmPassword.Text)
        {
            await DisplayAlertAsync("Error", "Passwords do not match.", "OK");
            return;
        }

        var newUser = new User
        {
            FullName = NewName.Text,
            Username = NewUserName.Text,
            Email =    NewEmail.Text,
            Password = NewPassword.Text,
        };

        try
        {
            await App.Database.RegisterUserAsync(newUser);

            var apiService = new DataAccess.WebService();
            bool serverSuccess = await apiService.PostUserToServer(newUser);

            if (!serverSuccess)
            {
                await DisplayAlertAsync("Warning", "Account created locally but failed to sync with server.", "OK");
            }

            await DisplayAlertAsync("Success", "Account created successfully!", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error", $"An error occurred: {ex.Message}", "OK");
            await Navigation.PopAsync();
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        // Navigate away using Cancel button
        await Navigation.PopAsync();
    }
}