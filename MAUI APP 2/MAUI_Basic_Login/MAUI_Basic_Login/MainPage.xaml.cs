namespace MAUI_Basic_Login
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnAdditionClicked(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(FirstValue.Text) || string.IsNullOrWhiteSpace(SecondValue.Text))
            {
                ResultLabel.Text = "Please enter both values.";
                return;
            }
            if (double.TryParse(FirstValue.Text, out double val1) && double.TryParse(SecondValue.Text, out double val2))
            {
                ResultLabel.Text = "";
                await Navigation.PushAsync(new MathDetailsPage(val1, val2, "Addition"));
            }

        }
        private async void OnSubtractionClicked(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(FirstValue.Text) || string.IsNullOrWhiteSpace(SecondValue.Text))
            {
                ResultLabel.Text = "Please enter both values.";
                return;
            }
            if (double.TryParse(FirstValue.Text, out double val1) && double.TryParse(SecondValue.Text, out double val2))
            {
                ResultLabel.Text = "";
                await Navigation.PushAsync(new MathDetailsPage(val1, val2, "Subtraction"));
            }

        }
    }
}
