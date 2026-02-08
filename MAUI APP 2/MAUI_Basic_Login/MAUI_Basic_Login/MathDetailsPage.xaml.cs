namespace MAUI_Basic_Login;

public partial class MathDetailsPage : ContentPage
{
	public MathDetailsPage(double firstNum, double secondNum, string operation)
	{
		InitializeComponent();

        NumsEntered.Text = $"The numbers you entered are {firstNum} and {secondNum}.";

		if (operation == "Addition")
		{
			double sum = firstNum + secondNum;
            ResultLabel.Text = $"The sum of the numbers is {sum}.";
        } else if (operation == "Subtraction") {
			double difference = firstNum - secondNum;
			ResultLabel.Text = $"The difference of the numbers is {difference}.";
        }
    }

	private async void OnGoBackClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
    }
}