namespace IT13_FinalProject;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        if (usernameEntry.Text == "admin" && passwordEntry.Text == "1234")
        {
            await DisplayAlert("Success", "Login successful!", "OK");
            // Set AdminPage as the new MainPage after login, wrapped in NavigationPage
            Application.Current.MainPage = new NavigationPage(new AdminPage());
        }
        else
        {
            await DisplayAlert("Error", "Invalid username or password.", "OK");
        }
    }
}