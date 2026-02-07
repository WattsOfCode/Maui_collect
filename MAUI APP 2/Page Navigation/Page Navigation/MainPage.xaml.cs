namespace Page_Navigation
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void OnSendMessageClicked(object sender, EventArgs e)
        {
           Navigation.PushAsync(new MessagePage(txtMessage.Text));
        }

    }
}
