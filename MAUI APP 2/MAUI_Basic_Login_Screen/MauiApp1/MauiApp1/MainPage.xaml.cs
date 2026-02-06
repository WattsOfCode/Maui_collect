namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        private const string CorrectUserName = "Deardorff";
        private const string CorrectPassword = "Password1";

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string enteredUsername = UsernameEntry.Text?.Trim();
            string enteredPassword = PasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(enteredUsername) || string.IsNullOrWhiteSpace(enteredPassword))
            {
                await DisplayAlertAsync("Error", "Please enter both username and password", "OK");
                return;
            }

            if (enteredUsername.Equals(CorrectUserName, StringComparison.OrdinalIgnoreCase) &&
                enteredPassword == CorrectPassword)
            {
                ResultLabel.Text = $"Login successful {enteredUsername}";
                ResultLabel.TextColor = Colors.Green;
                ResultLabel.IsVisible = true;
            }
            else
            {
                ResultLabel.Text = "Invalid username or password";
                ResultLabel.TextColor = Colors.Red;
                ResultLabel.IsVisible = true;

                PasswordEntry.Text = string.Empty;
            }
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            UsernameEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
            ResultLabel.IsVisible = false;
        }
    }
}
