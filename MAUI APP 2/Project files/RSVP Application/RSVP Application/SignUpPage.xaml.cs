using RSVP_Application.DataAccess;
using RSVP_Application.Models;  
namespace RSVP_Application;

public partial class SignUpPage : ContentPage
{
    public SignUpPage()
    {
        InitializeComponent();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        // Validate all fields have data
        if (string.IsNullOrWhiteSpace(NewName.Text) ||
            string.IsNullOrWhiteSpace(NewEmail.Text) ||
            string.IsNullOrWhiteSpace(NewPassword.Text) ||
            string.IsNullOrWhiteSpace(ConfirmPassword.Text))
        {
            await DisplayAlertAsync("Missing Info", "Please fill in all fields to continue.", "OK");
            return;
        }

        // Simple password match check
        if (NewPassword.Text != ConfirmPassword.Text)
        {
            await DisplayAlertAsync("Error", "Passwords do not match.", "OK");
            return;
        }

        // Navigate back to Login (Verification step)
        await DisplayAlertAsync("Success", "Account created! Please log in.", "OK");
        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        // Navigate away using Cancel button
        await Navigation.PopAsync();
    }
}